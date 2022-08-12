namespace Shao.ApiTemp.Domain.Dto.PromoteTask;

public class PromoteTaskIdReq : Req
{
    public long PromoteTaskId { get; set; }
}
public class PromoteTaskIdValitator : AbstractValidator<PromoteTaskIdReq>
{
    public PromoteTaskIdValitator()
    {
        RuleFor(x => x.PromoteTaskId).Must(x => x > 0).WithMessage("任务ID必须大于0");
    }
}
