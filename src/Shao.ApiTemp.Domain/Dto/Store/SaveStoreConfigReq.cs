namespace Shao.ApiTemp.Domain.Dto.Store;

public class SaveStoreConfigReq : StoreIdReq
{
    /// <summary>
    /// 多少天可购买 <see cref="PromoteLimitCount"/> 次
    /// </summary>
    public int PromoteLimitOfDay { get; set; }
    /// <summary>
    /// 推广限制次数
    /// </summary>
    public int PromoteLimitCount { get; set; }
}
public class SaveStoreConfigReqValitator : StoreIdReqValitator<SaveStoreConfigReq>
{
    public SaveStoreConfigReqValitator()
    {
        RuleFor(x => x.PromoteLimitOfDay).GreaterThan(0).WithName("购买天数");
        RuleFor(x => x.PromoteLimitCount).GreaterThan(0).WithName("购买次数");
    }
}
