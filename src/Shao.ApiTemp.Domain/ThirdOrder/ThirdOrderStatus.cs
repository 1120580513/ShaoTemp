namespace Shao.ApiTemp.Domain.ThirdOrder;

public enum ThirdOrderStatus
{
    /// <summary>
    /// 等待卖家发货
    /// </summary>
    [Display(Name =  "等待卖家发货")]
    WaitSellerSendGoods,
    /// <summary>
    /// 交易完成
    /// </summary>
    [Display(Name =  "交易完成")]
    TradeFinished,
}