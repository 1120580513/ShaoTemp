using Shao.ApiTemp.Domain.PromoteTask;

namespace Shao.ApiTemp.Domain.Dto.PromoteTask;

/// <summary>
///  查询推广任务请求
/// </summary>
public class QueryPromoteTaskReq : PageReq
{
    /// <summary>
    ///  
    /// </summary>
    public long? StoreId { get; set; }
    /// <summary>
    /// 推广任务名称 
    /// </summary>
    public string? PromoteTaskName { get; set; }
    /// <summary>
    /// 推广任务状态 
    /// </summary>
    public PromoteTaskStatus? PromoteTaskStatus { get; set; }
    /// <summary>
    /// 推广任务开始时间 
    /// </summary>
    public DateTime? StartTime { get; set; }
    /// <summary>
    /// 推广任务结束时间 
    /// </summary>
    public DateTime? EndTime { get; set; }
}
public class QueryPromoteTaskReqValitator : PageReqValidator<QueryPromoteTaskReq>
{
    public QueryPromoteTaskReqValitator()
    {
        RuleFor(x => x.StoreId).GreaterThan(0).WithMessage("店铺ID必须大于0")
            .When(x => x.StoreId is not null);
        RuleFor(x => x.PromoteTaskName).MaximumLength(128).WithMessage("推广任务名称最大为128")
            .When(x => x.PromoteTaskName is not null);
        RuleFor(x => x.PromoteTaskStatus).IsInEnum().WithMessage("推广任务状态错误")
            .When(x => x.PromoteTaskStatus is not null);
        RuleFor(x => x).Must(x => x.EndTime >= x.StartTime).WithMessage("结束时间必须大于开始时间")
            .When(x => x.StartTime is not null && x.EndTime is not null);
    }
}
