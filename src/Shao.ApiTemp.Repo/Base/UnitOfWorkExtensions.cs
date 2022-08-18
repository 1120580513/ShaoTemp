using Dapper;
using Dapper.Contrib.Extensions;

namespace Shao.ApiTemp.Repo.Base;

public static class UnitOfWorkExtensions
{
    public static async Task<R> InsertOrUpdateOrDelete<T>(
        this UnitOfWork unitOfWork, bool isInsert, bool isDelete, T persistent, string errMsg) where T : class
    {
        if (isDelete)
        {
            return R.Cond(
                await unitOfWork.Conn.DeleteAsync(persistent, unitOfWork.Tran, unitOfWork.CommandTimeout), errMsg);
        }
        return await InsertOrUpdate(unitOfWork, isInsert, persistent, errMsg);
    }
    private static async Task<R> InsertOrUpdate<T>(
        this UnitOfWork unitOfWork, bool isInsert, T persistent, string errMsg) where T : class
    {
        return R.Cond(isInsert
            ? (await unitOfWork.Conn.InsertAsync(persistent, unitOfWork.Tran, unitOfWork.CommandTimeout) > 0)
            : await unitOfWork.Conn.UpdateAsync(persistent, unitOfWork.Tran, unitOfWork.CommandTimeout),
        errMsg);
    }

    public static async Task<T> GetById<T, TKey>(this UnitOfWork unitOfWork, TKey id) where T : class
    {
        return await unitOfWork.Conn.GetAsync<T>(id, unitOfWork.Tran, unitOfWork.CommandTimeout);
    }
    public static async Task<T?> QuerySingle<T>(this UnitOfWork unitOfWork, string sql, object? param)
    {
        return await unitOfWork.Conn
            .QuerySingleOrDefaultAsync<T>(sql, param, unitOfWork.Tran, unitOfWork.CommandTimeout);
    }
    public static async Task<IEnumerable<T>> Query<T>(this UnitOfWork unitOfWork, string sql, object? param)
    {
        return await unitOfWork.Conn.QueryAsync<T>(sql, param, unitOfWork.Tran, unitOfWork.CommandTimeout);
    }
}

