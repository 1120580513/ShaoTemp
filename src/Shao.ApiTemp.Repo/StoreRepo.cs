using Shao.ApiTemp.Domain.Dto.Store;
using Shao.ApiTemp.Domain.Store;

namespace Shao.ApiTemp.Repo;

public class StoreRepo : DefaultConnRepo<StoreRepo>, IStoreRepo
{
    public async Task<StoreDo> Get(IStoreId req)
    {
        var storePo = await GetPersisent<StorePo, long>(req.StoreId, "店铺不存在");
        var store = await ToDo(storePo);
        return store;
    }

    public async Task<StoreDo?> GetByStoreName(string storeName)
    {
        var sql = @"
SELECT *
FROM dbo.Store WITH(NOLOCK)
WHERE StoreName = @StoreName
";
        StorePo? storePo = await QuerySingle<StorePo>(
            sql, new { StoreName = storeName }, nameof(GetByStoreName), "根据店铺名称查询失败");
        if (storePo is null) return null;

        var store = await ToDo(storePo!);
        return store;
    }

    public async Task<R<IEnumerable<QueryStoreDto>>> Query(QueryStoreReq req)
    {
        var sql = new SqlBuilder(@"
SELECT  {0}*
FROM dbo.Store WITH(NOLOCK)
{1}
")
            .Fill[0].Append("{0}")
            .Builder.Fill[1].Where
                .ParamAndLike(nameof(req.StoreName), !string.IsNullOrWhiteSpace(req.StoreName))
            .Builder.Build();
        return await PageQuery<QueryStoreDto>(
            sql, req, req, "ModifyOn DESC,StoreStatus DESC,StoreName ASC", nameof(Query), "查询店铺失败");
    }

    public async Task<R> Save(StoreDo store)
    {
        var storePo = App.Map<StoreDo, StorePo>(store);
        storePo.ModifyOn = DateTime.Now;
        await InsertOrUpdate(storePo, UnitOfWork.Default, "保存店铺失败");
        return R.Succ();
    }

    public async Task<R> SaveConfig(StoreDo store)
    {
        var storeConfigPo = App.Map<StoreConfigDo, StoreConfigPo>(store.Config!);
        storeConfigPo.StoreId = store.StoreId;
        await InsertOrUpdate(storeConfigPo, UnitOfWork.Default, "保存店铺配置失败");
        return R.Succ();
    }

    private async Task<StoreDo> ToDo(StorePo po)
    {
        var store = App.Map<StorePo, StoreDo>(po);
        var configSql = @"
SELECT *
FROM dbo.StoreConfig WITH(NOLOCK)
WHERE StoreId = @StoreId
";
        StoreConfigPo? config = await QuerySingle<StoreConfigPo>(configSql, po, nameof(ToDo), "查询店铺配置失败");
        store.Config = App.MapMaybeNull<StoreConfigPo, StoreConfigDo>(config);
        return store;
    }
}