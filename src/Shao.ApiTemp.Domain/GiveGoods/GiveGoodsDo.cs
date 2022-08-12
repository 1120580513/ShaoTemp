using Shao.ApiTemp.Domain.Dto.GiveGoods;

namespace Shao.ApiTemp.Domain.GiveGoods;

/// <summary>
/// 赠品 
/// </summary>
public class GiveGoodsDo : BaseDo, IEntity
{
    /// <summary>
    ///  
    /// </summary>
    public long GiveGoodsId { get; set; }
    /// <summary>
    /// 赠品名称 
    /// </summary>
    public string GiveGoodsName { get; set; }
    /// <summary>
    /// 赠品编码 
    /// </summary>
    public string GiveGoodsCode { get; set; }
    /// <summary>
    /// 赠送数量 
    /// </summary>
    public int GiveGoodsNum { get; set; }
    /// <summary>
    /// 赠品状态 
    /// </summary>
    public GiveGoodsStatus GiveGoodsStatus { get; set; }
    /// <summary>
    ///  
    /// </summary>
    public DateTime CreateOn { get; set; }

    /// <summary>
    /// 自动映射使用
    /// </summary>
    public GiveGoodsDo() { }
    public static async Task<GiveGoodsDo> Create(SaveGiveGoodsReq req)
    {
        var giveGoods = App.Map<SaveGiveGoodsReq, GiveGoodsDo>(req);
        giveGoods.GiveGoodsId = default;
        giveGoods.CreateOn = DateTime.Now;
        await giveGoods.EnsureGiveGoodsName();
        return giveGoods;
    }
    public async Task Update(SaveGiveGoodsReq req)
    {
        GiveGoodsName = req.GiveGoodsName!;
        GiveGoodsCode = req.GiveGoodsCode!;
        GiveGoodsNum = req.GiveGoodsNum;

        if (GiveGoodsName != req.GiveGoodsName)
        {
            await EnsureGiveGoodsName();
        }
    }
    public void Open()
    {
        AreEnsure(GiveGoodsStatus == GiveGoodsStatus.Off, "已开启，请刷新页面", GiveGoodsId, GiveGoodsStatus);
        GiveGoodsStatus = GiveGoodsStatus.On;
    }
    public void Close()
    {
        AreEnsure(GiveGoodsStatus == GiveGoodsStatus.On, "已关闭，请刷新页面", GiveGoodsId, GiveGoodsStatus);
        GiveGoodsStatus = GiveGoodsStatus.Off;
    }

    private async Task EnsureGiveGoodsName()
    {
        var giveGoods = await _repo.GetByGiveGoodsName(GiveGoodsName);
        AreEnsure(giveGoods is null, $"{GiveGoodsName} 已存在", GiveGoodsId, GiveGoodsName);
    }

    private readonly IGiveGoodsRepo _repo = new Lazy<IGiveGoodsRepo>(() => App.Resolve<IGiveGoodsRepo>()).Value;
}
