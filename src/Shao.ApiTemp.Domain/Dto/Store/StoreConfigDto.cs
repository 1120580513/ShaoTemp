#nullable disable

namespace Shao.ApiTemp.Domain.Dto.Store;

public class StoreConfigDto
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
