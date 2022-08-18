namespace Shao.ApiTemp.Domain.Dto.GiveGoods;

/// <summary>
///  查询赠品请求
/// </summary>
public class QueryGiveGoodsReq : PageReq
{
    /// <summary>
    /// 赠品名称 
    /// </summary>
    public string? GiveGoodsName { get; set; }
    /// <summary>
    /// 赠品编码 
    /// </summary>
    public string? GiveGoodsCode { get; set; }
}
public class QueryGiveGoodsReqValitator : PageReqValidator<QueryGiveGoodsReq>
{
    public QueryGiveGoodsReqValitator()
    {
        RuleFor(x => x.GiveGoodsName).Length(1, 128).WithName("赠品名称")
            .When(x => x.GiveGoodsName.IsNotEmpty());
        RuleFor(x => x.GiveGoodsCode).Length(1, 32).WithName("赠品编码")
            .When(x => x.GiveGoodsCode.IsNotEmpty());
    }
}
