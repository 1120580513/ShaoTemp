using Shao.ApiTemp.Domain.Dto.UserTask;

namespace Shao.ApiTemp.Controllers;

public class UserTaskController : ApiController
{
    private readonly IUserTaskService _userTaskService;

    public UserTaskController(IUserTaskService userTaskService)
    {
        _userTaskService = userTaskService;
    }

    [HttpPost]
    public async Task<R<IEnumerable<QueryUserTaskDto>>> Query(QueryUserTaskReq req)
    {
        return await _userTaskService.Query(req);
    }

    [HttpPost]
    public async Task<R> Claim(CliamUserTaskReq req)
    {
        return await _userTaskService.Claim(req);
    }
    [HttpPost]
    public async Task<R> Match(MatchUserTaskReq req)
    {
        return await _userTaskService.Match(req);
    }
}
