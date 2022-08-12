using Shao.ApiTemp.Domain.Dto.GiveGoods;

namespace Shao.ApiTemp.Controllers;

public class GiveGoodsController : ApiController
{
    private readonly IGiveGoodsService _giveGoodsService;

    public GiveGoodsController(IGiveGoodsService giveGoodsService)
    {
        _giveGoodsService = giveGoodsService;
    }

    [HttpPost]
    public async Task<R<IEnumerable<QueryGiveGoodsDto>>> Query(QueryGiveGoodsReq req)
    {
        return await _giveGoodsService.Query(req);
    }

    [HttpPost]
    public async Task<R> Save(SaveGiveGoodsReq req)
    {
        return await _giveGoodsService.Save(req);
    }
    [HttpPost]
    public async Task<R> Open(GiveGoodsIdReq req)
    {
        return await _giveGoodsService.Open(req);
    }
    [HttpPost]
    public async Task<R> Close(GiveGoodsIdReq req)
    {
        return await _giveGoodsService.Close(req);
    }
}
