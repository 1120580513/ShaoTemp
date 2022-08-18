using Shao.ApiTemp.Domain.Dto.GiveGoods;
using Shao.ApiTemp.Domain.GiveGoods;

namespace Shao.ApiTemp.Repo;

public class GiveGoodsRepo : DefaultConnRepo<GiveGoodsRepo>, IGiveGoodsRepo
{
    public async Task<GiveGoodsDo> Get(GiveGoodsIdReq req)
    {
        var giveGoodsPo = await GetPersisent<GiveGoodsPo, long>(req.GiveGoodsId, "赠品不存在");
        var giveGoods = await ToDo(giveGoodsPo);
        return giveGoods;
    }
    public async Task<GiveGoodsDo?> GetByGiveGoodsName(string giveGoodsName)
    {
        var sql = @"
SELECT * 
FROM dbo.GiveGoods WITH(NOLOCK)
WHERE GiveGoodsName = @GiveGoodsName
";
        var giveGoodsPo = await QuerySingle<GiveGoodsPo>(
            sql, new { GiveGoodsName = giveGoodsName }, nameof(GetByGiveGoodsName), "根据名称查询赠品失败");
        if (giveGoodsPo is not null) return null;

        var giveGoods = await ToDo(giveGoodsPo!);
        return giveGoods;
    }
    public async Task<R<IEnumerable<QueryGiveGoodsDto>>> Query(QueryGiveGoodsReq req)
    {
        var sql = new SqlBuilder(@"
SELECT  {0}*
FROM dbo.GiveGoods WITH(NOLOCK)
{1}
")
            .Fill[0].Append("{0}")
            .Builder.Fill[1]
            .Where.ParamAndLike(nameof(GiveGoodsPo.GiveGoodsName), req.GiveGoodsName.IsNotEmpty())
            .Where.ParamAnd(nameof(GiveGoodsPo.GiveGoodsCode), req.GiveGoodsCode.IsNotEmpty())
            .Builder.Build();
        return await PageQuery<QueryGiveGoodsDto>(
            sql, req, req, "GiveGoodsId DESC,GiveGoodsName ASC", nameof(Query), "查询赠品失败");
    }

    public async Task<R> Save(GiveGoodsDo giveGoods, UnitOfWork connContext)
    {
        var giveGoodsPo = App.Map<GiveGoodsDo, GiveGoodsPo>(giveGoods);
        await InsertOrUpdate(giveGoodsPo, connContext, "保存赠品失败");
        return R.Succ();
    }

    private async Task<GiveGoodsDo> ToDo(GiveGoodsPo po)
    {
        var giveGoods = App.Map<GiveGoodsPo, GiveGoodsDo>(po);
        return await Task.FromResult(giveGoods);
    }
}

