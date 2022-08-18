using Shao.ApiTemp.Domain.Dto.GiveGoods;

namespace Shao.ApiTemp.Domain.GiveGoods;

public interface IGiveGoodsRepo : IRepository<GiveGoodsDo>
{
    Task<GiveGoodsDo> Get(GiveGoodsIdReq req);
    Task<GiveGoodsDo?> GetByGiveGoodsName(string giveGoodsName);
    Task<R<IEnumerable<QueryGiveGoodsDto>>> Query(QueryGiveGoodsReq req);
}

