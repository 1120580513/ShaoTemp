namespace Shao.ApiTemp.Domain.UserTask;

public enum UserTaskStatus
{
    /// <summary>
    /// 退款失败
    /// </summary>
    [Display(Name = "退款失败")]
    RefundFail = -2,
    /// <summary>
    /// 已作废
    /// </summary>
    [Display(Name = "已作废")]
    Canceled = -1,
    /// <summary>
    /// 待匹配
    /// </summary>
    [Display(Name = "待匹配")]
    WaitMatch = 0,
    /// <summary>
    /// 待审核
    /// </summary>
    [Display(Name = "待审核")]
    WaitAudit = 1,
    /// <summary>
    /// 待退款
    /// </summary>
    [Display(Name = "待退款")]
    WaitRefund = 2,
    /// <summary>
    /// 已完成
    /// </summary>
    [Display(Name = "已完成")]
    Finished = 4,
}
