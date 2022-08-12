using Shao.ApiTemp.Domain.PromoteTask;

namespace Shao.ApiTemp.Domain.Dto.PromoteTask;

/// <summary>
///  推广任务
/// </summary>
public class PromoteTaskDto
{
    /// <summary>
    ///  
    /// </summary>
    public long PromoteTaskId { get; set; }
    /// <summary>
    ///  
    /// </summary>
    public long StoreId { get; set; }
    /// <summary>
    ///  
    /// </summary>
    public string StoreName { get; set; }
    /// <summary>
    /// 推广任务名称 
    /// </summary>
    public string PromoteTaskName { get; set; }
    /// <summary>
    /// 推广任务状态 
    /// </summary>
    public PromoteTaskStatus PromoteTaskStatus { get; set; }
    /// <summary>
    /// 推广任务开始时间 
    /// </summary>
    public DateTime StartTime { get; set; }
    /// <summary>
    /// 推广任务结束时间 
    /// </summary>
    public DateTime EndTime { get; set; }
    /// <summary>
    /// 规格
    /// </summary>
    public IEnumerable<PromoteTaskSpecDto> Specs { get; set; }
}
