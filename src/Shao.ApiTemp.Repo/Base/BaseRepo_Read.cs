namespace Shao.ApiTemp.Repo.Base;

public partial class BaseRepo
{
    protected async Task<TPersisent> GetPersisent<TPersisent, TPersisentKey>(TPersisentKey id, string errMsg)
        where TPersisent : class, IPersistant
    {
        var connContext = EnsureUnitOfWork();
        return await Template(async () =>
        {
            var persisent = await connContext.GetById<TPersisent, TPersisentKey>(id);
            AreEnsure(persisent is not null, errMsg, id);
            return persisent!;
        }, nameof(GetPersisent), errMsg, id);
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
        var connContext = EnsureUnitOfWork();
        return await Template(async () =>
        {
            return await connContext.QuerySingle<T>(sql, param);
        }, nameof(GetPersisent), errMsg, sql, param);
    }

    protected async Task<IEnumerable<T>> Query<T>(string sql, object? param, string methodName, string errMsg)
    {
        var connContext = EnsureUnitOfWork();
        return await Template(async () =>
        {
            return await connContext.Query<T>(sql, param);
        }, nameof(GetPersisent), errMsg, sql, param);
    }

    /// <summary>
    /// sql: "SELECT {0}* FROM TableName"
    /// orderBy: "Id ASC"
    /// </summary>
    protected async Task<R<IEnumerable<T>>> PageQuery<T>(
        string sql, object? param, PageReq pageReq, string orderBy, string methodName, string errMsg)
    {
        var connContext = EnsureUnitOfWork();
        return await Template(async () =>
        {
            var pageCountSql = $@"SELECT COUNT(*)
FROM ( {string.Format(sql, string.Empty)}
) __page
";
            var total = await connContext.QuerySingle<int>(pageCountSql, param);
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
            var data = await connContext.Query<T>(dataSql, param);
            return R.Succ(data, pageR);
        }, nameof(GetPersisent), errMsg, sql, param);
    }
}