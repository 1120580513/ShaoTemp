using Shao.ApiTemp.Domain.PromoteTask;

namespace Shao.ApiTemp.Domain.Dto.PromoteTask;

public class PromoteTaskSpecIdReq : IPromoteTaskSpecId
{
    public long PromoteTaskSpecId { get; set; }

    public PromoteTaskSpecIdReq() { }
    public PromoteTaskSpecIdReq(long id) 
    {
        PromoteTaskSpecId = id;
    }
}
public class PromoteTaskSpecIdReqValitator : AbstractValidator<PromoteTaskSpecIdReq>
{
    public PromoteTaskSpecIdReqValitator()
    {
        RuleFor(x => x.PromoteTaskSpecId).GreaterThan(0).WithName("任务标识");
    }
}
