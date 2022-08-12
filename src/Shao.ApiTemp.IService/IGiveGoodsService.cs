using Shao.ApiTemp.Domain.Dto.GiveGoods;

namespace Shao.ApiTemp.IService;

public interface IGiveGoodsService : IAppService
{
    Task<R<IEnumerable<QueryGiveGoodsDto>>> Query(QueryGiveGoodsReq req);

    Task<R> Save(SaveGiveGoodsReq req);
    Task<R> Open(GiveGoodsIdReq req);
    Task<R> Close(GiveGoodsIdReq req);
}
