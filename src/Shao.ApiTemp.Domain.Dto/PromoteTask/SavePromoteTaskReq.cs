using Shao.ApiTemp.Common.Interface;

namespace Shao.ApiTemp.Domain.Dto.PromoteTask;

public class SavePromoteTaskReq : PromoteTaskIdReq, ISaveIsInsert
{
    public string? PromoteTaskName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public bool IsInsert() => PromoteTaskId == default;
}
public class SavePromoteTaskValitator : AbstractValidator<SavePromoteTaskReq>
{
    public SavePromoteTaskValitator()
    {
        RuleFor(x => x.PromoteTaskName).NotEmpty().WithMessage("任务名不能为空");
        RuleFor(x => x).Must(x => x.StartTime < x.EndTime).WithName("StartTime - EndTime")
            .WithMessage(x => $"结束时间[{x.EndTime}] 不能小于 开始时间[{x.StartTime}]");
    }
}
