namespace Shao.ApiTemp.Domain.PromoteTask;

public enum PromoteTaskStatus
{
    /// <summary>
    /// 已删除
    /// </summary>
    [Display(Name = "已删除")]
    Deleted = -1,
    /// <summary>
    /// 待发布
    /// </summary>
    [Display(Name = "待发布")]
    Unpublished = 0,
    /// <summary>
    /// 已发布
    /// </summary>
    [Display(Name = "已发布")]
    Published = 1,
    /// <summary>
    /// 已结束
    /// </summary>
    [Display(Name = "已结束")]
    Closed = 2,
}
