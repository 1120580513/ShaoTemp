using Dapper;
using Dapper.Contrib.Extensions;

namespace Shao.ApiTemp.Repo.Base;

public partial class BaseRepo
{
    protected async Task<TPersisent> GetPersisent<TPersisent, TPersisentKey>(TPersisentKey id, string errMsg)
        where TPersisent : class, IPersistant
    {
        TPersisent? r = await Template(
            async conn => await conn.GetAsync<TPersisent>(id), nameof(GetPersisent), errMsg, id);
        return Ensure(r, errMsg);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="methodName"></param>
    /// <returns></returns>
    ///<exception cref="Exception"></exception>
    protected async Task<T?> QuerySingle<T>(string sql, object? param, string methodName, string errMsg)
    {
        return await Template(async conn =>
        {
            return await conn.QuerySingleOrDefaultAsync<T>(sql, param, transaction: null, commandTimeout: DefaultTimout);
        }, methodName, errMsg, sql, param);
    }

    protected async Task<IEnumerable<T>> Query<T>(string sql, object? param, string methodName, string errMsg)
    {
        return await Template(async conn =>
        {
            return await conn.QueryAsync<T>(sql, param, transaction: null, commandTimeout: DefaultTimout);
        }, methodName, errMsg, sql);
    }

    /// <summary>
    /// sql: "SELECT {0}* FROM TableName"
    /// orderBy: "Id ASC"
    /// </summary>
    protected async Task<R<IEnumerable<T>>> PageQuery<T>(
        string sql, object? param, PageReq pageReq, string orderBy, string methodName, string errMsg)
    {
        var pageCountSql = $@"SELECT COUNT(*)
FROM ( {string.Format(sql, string.Empty)}
) __page
";
        int total = await Template(async conn =>
        {
            return await conn.QuerySingleAsync<int>(
                pageCountSql, param, transaction: null, commandTimeout: DefaultTimout);
        }, methodName, errMsg, pageCountSql, sql);
        var pageR = new PageR(pageReq, total);
        if (total == default)
        {
            return R.Succ(Enumerable.Empty<T>(), pageR);
        }

        var dataSql = $@"SELECT *
FROM ( {string.Format(sql, $"__rowid = ROW_NUMBER() OVER(ORDER BY {orderBy}),")}
) __page
WHERE __rowid >= {pageReq.GetMinRowNo()} AND __rowid <= {pageReq.GetMaxRowNo()}
";
        var data = await Template(async conn =>
        {
            return await conn.QueryAsync<T>(dataSql, param, transaction: null, commandTimeout: DefaultTimout);
        }, methodName, errMsg, dataSql, sql);
        return R.Succ(data, pageR);
    }
}