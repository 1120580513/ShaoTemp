namespace Shao.ApiTemp.Domain.Dto.Store;

/// <summary>
///  保存店铺请求
/// </summary>
public class SaveStoreReq : StoreIdReq, ISaveIsInsert
{
    /// <summary>
    /// 店铺名称 
    /// </summary>
    public string? StoreName { get; set; }
    /// <summary>
    /// 审核限额 
    /// </summary>
    public decimal AuditQuota { get; set; }

    public bool IsInsert() => StoreId == default;
}
public class SaveStoreReqValitator : ReqValidator<SaveStoreReq>
{
    public SaveStoreReqValitator()
    {
        RuleFor(x => x.StoreId).GreaterThanOrEqualTo(0).WithName("店铺标识");
        RuleFor(x => x.StoreName).NotEmpty().Length(1, 128).WithName("店铺名称");
        RuleFor(x => x.AuditQuota).GreaterThan(1).WithName("审核限额");
    }
}
