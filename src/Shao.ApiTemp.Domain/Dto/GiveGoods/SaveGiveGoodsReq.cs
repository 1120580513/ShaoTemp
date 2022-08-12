using Shao.ApiTemp.Domain.GiveGoods;

namespace Shao.ApiTemp.Domain.Dto.GiveGoods;

/// <summary>
///  保存赠品请求
/// </summary>
public class SaveGiveGoodsReq : GiveGoodsIdReq
{
    /// <summary>
    /// 赠品名称 
    /// </summary>
    ///<remarks>nvarchar(128)</remarks>
    public string? GiveGoodsName { get; set; }
    /// <summary>
    /// 赠品编码 
    /// </summary>
    ///<remarks>varchar(32)</remarks>
    public string? GiveGoodsCode { get; set; }
    /// <summary>
    /// 赠送数量 
    /// </summary>
    ///<remarks>int(4)</remarks>
    public int GiveGoodsNum { get; set; }
    /// <summary>
    /// 赠品状态 
    /// </summary>
    ///<remarks>int(4)</remarks>
    public GiveGoodsStatus GiveGoodsStatus { get; set; }

    public bool IsInsert() => GiveGoodsId == default;
}
public class SaveGiveGoodsReqValitator : ReqValidator<SaveGiveGoodsReq>
{
    public const int GiveGoodsName_Length = 128;
    public const int GiveGoodsCode_Length = 32;
    public SaveGiveGoodsReqValitator()
    {
        RuleFor(x => x.GiveGoodsId).GreaterThanOrEqualTo(0).WithName("赠品标识");
        RuleFor(x => x.GiveGoodsName).NotEmpty().Length(1, GiveGoodsName_Length).WithName("赠品名称");
        RuleFor(x => x.GiveGoodsCode).NotEmpty().Length(1, GiveGoodsCode_Length).WithName("赠品编码");
        RuleFor(x => x.GiveGoodsNum).GreaterThan(0).WithName("赠送数量");
        RuleFor(x => x.GiveGoodsStatus).IsInEnum().WithName("赠品状态");
    }
}
