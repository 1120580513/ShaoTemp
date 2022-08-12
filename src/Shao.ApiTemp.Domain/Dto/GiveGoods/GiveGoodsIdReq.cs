namespace Shao.ApiTemp.Domain.Dto.GiveGoods;

/// <summary>
///  赠品标识
/// </summary>
public class GiveGoodsIdReq : Req
{
    /// <summary>
    ///  
    /// </summary>
    public long GiveGoodsId { get; set; }

    public GiveGoodsIdReq() { }
    public GiveGoodsIdReq(long giveGoodsId)
    {
        GiveGoodsId = giveGoodsId;
    }
}
public class GiveGoodsIdReqValitator : ReqValidator<GiveGoodsIdReq>
{
    public GiveGoodsIdReqValitator()
    {
        RuleFor(x => x.GiveGoodsId).GreaterThan(0).WithName("赠品标识");
    }
}
public class GiveGoodsIdReqValitator<TReq> : ReqValidator<TReq>
    where TReq : GiveGoodsIdReq
{
    public GiveGoodsIdReqValitator()
    {
        RuleFor(x => x.GiveGoodsId).GreaterThan(0).WithName("赠品标识");
    }
}
