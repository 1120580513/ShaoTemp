using System.Data;

namespace Shao.ApiTemp.Common.Interface;

public class UnitOfWork : IDisposable
{
    private UnitOfWork()
    {
        CommandTimeout = 10;
    }

    public UnitOfWork(IDbConnection conn, IDbTransaction? tran = null) : this()
    {
        Conn = conn;
        Tran = tran;
    }

    public IDbConnection? Conn { get; private set; }
    public IDbTransaction? Tran { get; private set; }
    public int CommandTimeout { get; set; }

    public void Rollback(string msg, params object[] args)
    {
        Tran!.Rollback();
        throw new CustomException(msg, args);
    }

    public void Commit()
    {
        Tran!.Commit();
    }

    public static readonly UnitOfWork Default = new UnitOfWork();

    public void Dispose()
    {
        Conn?.Dispose();
        Tran?.Dispose();
    }
}