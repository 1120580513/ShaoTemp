using Shao.ApiTemp.Domain.UserTask;

namespace Shao.ApiTemp.Domain.Dto.UserTask;

/// <summary>
///  查询用户任务请求
/// </summary>
public class QueryUserTaskReq : PageReq
{
    /// <summary>
    ///  
    /// </summary>
    public long? PromoteTaskId { get; set; }
    /// <summary>
    ///  
    /// </summary>
    public long? StoreId { get; set; }
    /// <summary>
    /// 用户任务状态 
    /// </summary>
    public UserTaskStatus? UserTaskStatus { get; set; }
    /// <summary>
    /// 用户手机号 
    /// </summary>
    public string? Mobile { get; set; }
    /// <summary>
    /// 订单号 
    /// </summary>
    public string? OrderNo { get; set; }
    /// <summary>
    /// 推广任务开始时间 
    /// </summary>
    public DateTime? StartTime { get; set; }
    /// <summary>
    /// 推广任务结束时间 
    /// </summary>
    public DateTime? EndTime { get; set; }
}
public class QueryUserTaskReqValitator : PageReqValidator<QueryUserTaskReq>
{
    public QueryUserTaskReqValitator()
    {
        RuleFor(x => x.PromoteTaskId).GreaterThan(0).WithName("推广任务标识")
            .When(x => x.PromoteTaskId.HasValue);
        RuleFor(x => x.StoreId).GreaterThan(0).WithName("店铺标识")
            .When(x => x.StoreId.HasValue);
        RuleFor(x => x.UserTaskStatus).IsInEnum().WithName("用户任务状态")
            .When(x => x.UserTaskStatus.HasValue);
        RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime).WithMessage("结束时间必须大于开始时间")
            .When(x => x.EndTime.HasValue && x.StartTime.HasValue);
    }
}
