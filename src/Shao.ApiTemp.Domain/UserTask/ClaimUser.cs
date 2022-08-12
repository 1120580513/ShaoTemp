namespace Shao.ApiTemp.Domain.UserTask;

public class ClaimUser
{
    /// <summary>
    /// 手机号 
    /// </summary>
    public string Mobile { get; set; }
}
public class ClaimUserValitator : AbstractValidator<ClaimUser>
{
    public ClaimUserValitator()
    {
        RuleFor(x => x.Mobile).NotEmpty().Length(11, 11).WithName("手机号");
    }
}
