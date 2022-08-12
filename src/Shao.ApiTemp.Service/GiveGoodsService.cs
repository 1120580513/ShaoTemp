using Shao.ApiTemp.Domain.Dto.GiveGoods;
using Shao.ApiTemp.Domain.GiveGoods;

namespace Shao.ApiTemp.Service;

public class GiveGoodsService : BaseService, IGiveGoodsService
{
    private readonly IGiveGoodsRepo _giveGoodsRepo;

    public GiveGoodsService(IGiveGoodsRepo giveGoodsRepo)
    {
        _giveGoodsRepo = giveGoodsRepo;
    }

    public async Task<R<IEnumerable<QueryGiveGoodsDto>>> Query(QueryGiveGoodsReq req)
    {
        var result = await _giveGoodsRepo.Query(req);
        return result;
    }

    public async Task<R> Save(SaveGiveGoodsReq req)
    {
        GiveGoodsDo giveGoods;
        if (req.IsInsert())
        {
            giveGoods = await GiveGoodsDo.Create(req);
        }
        else
        {
            giveGoods = await _giveGoodsRepo.Get(req);
            await giveGoods.Update(req);
        }
        return await _giveGoodsRepo.Save(giveGoods);
    }
    public async Task<R> Open(GiveGoodsIdReq req)
    {
        var giveGoods = await _giveGoodsRepo.Get(req);
        giveGoods.Open();
        return await _giveGoodsRepo.Save(giveGoods);
    }
    public async Task<R> Close(GiveGoodsIdReq req)
    {
        var giveGoods = await _giveGoodsRepo.Get(req);
        giveGoods.Close();
        return await _giveGoodsRepo.Save(giveGoods);
    }
}
