namespace Shao.ApiTemp.Domain.Dto.UserTask;

/// <summary>
///  唯一用户任务
/// </summary>
public class UserTaskIdReq : Req
{
    /// <summary>
    ///  
    /// </summary>
    public long UserTaskId {get;set;} 
}
public class UserTaskIdReqValitator : AbstractValidator<UserTaskIdReq>
{
    public UserTaskIdReqValitator()
    {
        RuleFor(x => x.UserTaskId).Must(x => x > 0).WithMessage("UserTaskId不能为空");
    }
}
