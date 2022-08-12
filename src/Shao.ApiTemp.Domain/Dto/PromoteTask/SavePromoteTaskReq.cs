namespace Shao.ApiTemp.Domain.Dto.PromoteTask;

/// <summary>
///  保存推广任务请求
/// </summary>
public class SavePromoteTaskReq : PromoteTaskIdReq
{
    /// <summary>
    ///  
    /// </summary>
    public long StoreId { get; set; }
    /// <summary>
    /// 推广任务名称 
    /// </summary>
    public string PromoteTaskName { get; set; }
    /// <summary>
    /// 推广任务开始时间 
    /// </summary>
    public DateTime StartTime { get; set; }
    /// <summary>
    /// 推广任务结束时间 
    /// </summary>
    public DateTime EndTime { get; set; }
    /// <summary>
    /// 任务规格
    /// </summary>
    public IEnumerable<SavePromoteTaskSpecReq> Specs { get; set; }

    public bool IsInsert() => PromoteTaskId == default;
}
public class SavePromoteTaskReqValitator : AbstractValidator<SavePromoteTaskReq>
{
    public SavePromoteTaskReqValitator()
    {
        RuleFor(x => x.StoreId).GreaterThan(0).WithName("店铺标识");
        RuleFor(x => x.PromoteTaskName).NotEmpty().MaximumLength(128).WithName("推广任务名称");
        RuleFor(x => x.StartTime).LessThan(x => x.EndTime).WithName("开始时间");
        RuleFor(x => x.Specs).NotNull().NotEmpty()
            .ForEach(x => x.SetValidator(new PromoteTaskSpecDtoValidator())).WithName("任务规格");
        RuleFor(x => x.Specs)
            .Must(SpecsNumUnique).WithMessage("任务规格数必须唯一").When(x => x.Specs?.Any() ?? false);
    }

    private bool SpecsNumUnique(IEnumerable<SavePromoteTaskSpecReq> specs)
    {
        return !specs.Where(x => !x.IsDelete).Select(x => x.SpecNum).GroupBy(x => x).Where(g => g.Count() > 1).Any();
    }
}
