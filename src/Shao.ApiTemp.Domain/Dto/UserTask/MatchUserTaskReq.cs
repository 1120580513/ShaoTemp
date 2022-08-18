using Shao.ApiTemp.Domain.UserTask;

namespace Shao.ApiTemp.Domain.Dto.UserTask;

public class MatchUserTaskReq : Req, IUserTaskId
{
    public long UserTaskId { get; set; }
    public string OrderNo { get; set; }
}
public class MatchUserTaskReqValidator : AbstractValidator<MatchUserTaskReq>
{
    public MatchUserTaskReqValidator()
    {
        RuleFor(x => x.UserTaskId).GreaterThan(0).WithName("用户任务标识");
        RuleFor(x => x.OrderNo).NotEmpty().WithName("订单号");
    }
}