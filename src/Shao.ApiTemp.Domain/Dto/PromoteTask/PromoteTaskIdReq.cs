using Shao.ApiTemp.Domain.PromoteTask;

namespace Shao.ApiTemp.Domain.Dto.PromoteTask;

/// <summary>
///  唯一推广任务
/// </summary>
public class PromoteTaskIdReq : Req, IPromoteTaskId
{
    /// <summary>
    ///  
    /// </summary>
    public long PromoteTaskId { get; set; }

    public PromoteTaskIdReq() { }
    public PromoteTaskIdReq(long id)
    {
        PromoteTaskId = id;
    }
}
public class PromoteTaskIdReqValitator : AbstractValidator<PromoteTaskIdReq>
{
    public PromoteTaskIdReqValitator()
    {
        RuleFor(x => x.PromoteTaskId).GreaterThan(0).WithName("推广任务标识");
    }
}
