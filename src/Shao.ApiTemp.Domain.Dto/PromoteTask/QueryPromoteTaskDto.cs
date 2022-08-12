using Shao.ApiTemp.Domain.PromoteTask;

namespace Shao.ApiTemp.Domain.Dto.PromoteTask;

public class QueryPromoteTaskDto
{
    public long PromoteTaskId { get; set; }
    public string PromoteTaskName { get; set; }
    public PromoteTaskStatus Status { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
