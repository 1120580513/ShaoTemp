namespace Shao.ApiTemp.Domain.Dto.UserTask;

public record QueryUserTaskDto
{
    public long UserTaskId { get; set; }
    public string UserTaskName { get; set; }
}
