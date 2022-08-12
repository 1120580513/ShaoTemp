namespace Shao.ApiTemp.Domain.Store;

public class StoreConfigDo
{
    public long StoreConfigId { get; set; }
    /// <summary>
    /// 多少天可购买 <see cref="PromoteLimitCount"/> 次
    /// </summary>
    public int PromoteLimitOfDay { get; set; }
    /// <summary>
    /// 推广限制次数
    /// </summary>
    public int PromoteLimitCount { get; set; }

    /// <summary>
    /// 自动映射使用
    /// </summary>
    public StoreConfigDo() { }
}
