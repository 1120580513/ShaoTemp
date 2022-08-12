using Shao.ApiTemp.Common.Exceptions;
using System.Data;
using System.Data.SqlClient;

namespace Shao.ApiTemp.Repo.Base;

public partial class BaseRepo : IEnsure
{
    protected const int DefaultTimout = 10;
    private const string DefaultErrMsg = "仓储操作失败";
    protected readonly ICustomLog Log;
    private readonly string _connStr;

    public BaseRepo(string connStr, ICustomLog log)
    {
        AreEnsure(connStr.IsNotEmpty(), "连接字符串不能为空");

        Log = log;
        _connStr = connStr;
    }

    protected async Task<R> TranTemplate(
        Func<IDbConnection, IDbTransaction, Task> func, string methodName, string? errMsg, params object[] args)
    {
        try
        {
            using var conn = CreateDbConnection();
            conn.Open();
            using var tran = CreateDbTransaction(conn);
            await func(conn, tran);
            tran.Commit();
            return R.Succ();
        }
        catch(RepositoryException repoEx)
        {
            Debug.Assert(false);
            Log.Error(methodName, repoEx, args);
            throw repoEx;
        }
        catch (Exception ex)
        {
            Debug.Assert(false);
            Log.Error(methodName, ex, args);
            throw new RepositoryException(errMsg ?? DefaultErrMsg, ex, methodName, args);
        }
    }
    private async Task<T> Template<T>(
        Func<IDbConnection, Task<T>> func, string methodName, string? errMsg, params object[] args)
    {
        try
        {
            using var conn = CreateDbConnection();
            return await func(conn);
        }
        catch (Exception ex)
        {
            Debug.Assert(false);
            Log.Error(methodName, ex, args);
            throw new RepositoryException(errMsg ?? DefaultErrMsg, ex, methodName, args);
        }
    }
    private async Task Template(
        Func<IDbConnection, Task> func, string methodName, string? errMsg, params object[] args)
    {
        try
        {
            using var conn = CreateDbConnection();
            await func(conn);
        }
        catch (Exception ex)
        {
            Debug.Assert(false);
            Log.Error(methodName, ex, args);
            throw new RepositoryException(errMsg ?? DefaultErrMsg, ex, methodName, args);
        }
    }

    protected TPersisent Ensure<TPersisent>(TPersisent? persisent, string message)
        where TPersisent : IPersistant
    {
        AreEnsure(persisent is not null, message);
        return persisent!;
    }
    /// <inheritdoc />
    /// <exception cref="RepositoryException" />
    public void AreEnsure(bool condition, string msg, params object[] args)
    {
        if (!condition) throw new RepositoryException(msg, args);
    }

    private IDbConnection CreateDbConnection()
    {
        return new SqlConnection(_connStr);
    }
    private IDbTransaction CreateDbTransaction(IDbConnection conn)
    {
        return conn.BeginTransaction(IsolationLevel.ReadCommitted);
    }
}