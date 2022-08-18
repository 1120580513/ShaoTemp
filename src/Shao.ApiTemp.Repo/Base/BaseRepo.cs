using Shao.ApiTemp.Common.Exceptions;
using System.Data;
using System.Data.SqlClient;

namespace Shao.ApiTemp.Repo.Base;

public partial class BaseRepo : IEnsure
{
    protected readonly ICustomLog Log;
    private readonly string _connStr;

    public BaseRepo(string connStr, ICustomLog log)
    {
        AreEnsure(connStr.IsNotEmpty(), "连接字符串不能为空");

        Log = log;
        _connStr = connStr;
    }

    protected async Task<R> TranTemplate(
        UnitOfWork unitOfWork,
        Func<UnitOfWork, Task> func, string methodName, string errMsg, params object[] args)
    {
        try
        {
            unitOfWork = EnsureTranUnitOfWork(unitOfWork);
            await func(unitOfWork);
            unitOfWork.Commit();
            return R.Succ();
        }
        catch (RepositoryException repoEx)
        {
            throw repoEx;
        }
        catch (Exception ex)
        {
            Debug.Assert(false);
            Log.Error(methodName, ex, args);
            throw new RepositoryException(errMsg, ex, methodName, args);
        }
    }

    private async Task<T> Template<T>(
        Func<Task<T>> func, string methodName, string? errMsg, params object[] args)
    {
        try
        {
            return await func();
        }
        catch (Exception ex)
        {
            Debug.Assert(false);
            Log.Error(methodName, ex, args);
            throw new RepositoryException(errMsg, ex, methodName, args);
        }
    }
    private async Task Template(
        Func<Task> func, string methodName, string? errMsg, params object[] args)
    {
        try
        {
            await func();
        }
        catch (Exception ex)
        {
            Debug.Assert(false);
            Log.Error(methodName, ex, args);
            throw new RepositoryException(errMsg, ex, methodName, args);
        }
    }
    /// <returns>返回一个必定有连接的工作单元</returns>
    protected UnitOfWork EnsureUnitOfWork(UnitOfWork? connContext = null)
    {
        if (NeedInitUnitOfWork(connContext))
        {
            var conn = CreateDbConnection();
            return new UnitOfWork(conn);
        }
        return connContext!;
    }
    /// <returns>返回一个必定有事务的工作单元</returns>
    protected UnitOfWork EnsureTranUnitOfWork(UnitOfWork? connContext)
    {
        if (NeedInitUnitOfWork(connContext))
        {
            return CreateTranUnitOfWork();
        }
        return connContext!;
    }
    /// <inheritdoc />
    public UnitOfWork CreateTranUnitOfWork()
    {
        var conn = CreateDbConnection();
        conn.Open();
        var tran = CreateDbTransaction(conn);
        return new UnitOfWork(conn, tran);
    }
    private IDbConnection CreateDbConnection()
    {
        return new SqlConnection(_connStr);
    }
    private IDbTransaction CreateDbTransaction(IDbConnection conn)
    {
        return conn.BeginTransaction(IsolationLevel.ReadCommitted);
    }
    private bool NeedInitUnitOfWork(UnitOfWork? unitOfWork)
    {
        return unitOfWork is null
            || ReferenceEquals(unitOfWork, UnitOfWork.Default)
            || unitOfWork.Conn is null;
    }

    /// <inheritdoc />
    /// <exception cref="RepositoryException" />
    public void AreEnsure(bool condition, string msg, params object[] args)
    {
        if (!condition) throw new RepositoryException(msg, args);
    }
}