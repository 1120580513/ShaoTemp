using static Shao.ApiTemp.Domain.Dto.GiveGoods.SaveGiveGoodsReqValitator;

namespace Shao.ApiTemp.Domain.Dto.PromoteTask;

/// <summary>
///  
/// </summary>
public class SavePromoteTaskSpecReq
{
    /// <summary>
    ///  
    /// </summary>
    public long PromoteTaskSpecId { get; set; }
    /// <summary>
    ///  
    /// </summary>
    public int SpecNum { get; set; }
    /// <summary>
    ///  
    /// </summary>
    public long GiveGoodsId { get; set; }
    /// <summary>
    ///  
    /// </summary>
    public string GiveGoodsName { get; set; }
    /// <summary>
    ///  
    /// </summary>
    public string GiveGoodsCode { get; set; }
    /// <summary>
    ///  
    /// </summary>
    public int GiveGoodsNum { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool IsDelete { get; set; }


    public bool IsInsert => PromoteTaskSpecId == default;
}
public class PromoteTaskSpecDtoValidator : AbstractValidator<SavePromoteTaskSpecReq>
{
    public PromoteTaskSpecDtoValidator()
    {
        RuleFor(x => x.SpecNum).GreaterThan(0).WithName("规格数");
        RuleFor(x => x.GiveGoodsId).GreaterThan(0).WithName("赠品标识");
        RuleFor(x => x.GiveGoodsName).Length(1, GiveGoodsName_Length).WithName("赠品名称");
        RuleFor(x => x.GiveGoodsCode).Length(1, GiveGoodsName_Length).WithName("赠品编码");
        RuleFor(x => x.GiveGoodsNum).GreaterThan(0).WithName("赠品数量");
    }
}

