using Shao.ApiTemp.Domain.PromoteTask;
using Shao.ApiTemp.Domain.UserTask;

namespace Shao.ApiTemp.Domain.Dto.UserTask;

/// <summary>
///  登记用户任务
/// </summary>
public class CliamUserTaskReq : IPromoteTaskSpecId
{
    /// <summary>
    ///  
    /// </summary>
    public long PromoteTaskSpecId { get; set; }
    /// <summary>
    /// 领取用户
    /// </summary>
    public ClaimUser ClaimUser { get; set; }
}
public class RegisterUserTaskReqValitator : AbstractValidator<CliamUserTaskReq>
{
    public RegisterUserTaskReqValitator()
    {
        RuleFor(x => x.PromoteTaskSpecId).GreaterThan(0).WithName("任务规格");
        RuleFor(x => x.ClaimUser).NotNull().SetValidator(new ClaimUserValitator()).WithName("用户");
    }
}
