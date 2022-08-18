using Shao.ApiTemp.Domain.Dto.UserTask;
using Shao.ApiTemp.Domain.UserTask;
using Shao.ApiTemp.DomainService;

namespace Shao.ApiTemp.Service;

public class UserTaskService : BaseService, IUserTaskService
{
    private readonly MatchUserTaskService _matchUserTaskService;
    private readonly IUserTaskRepo _userTaskRepo;

    public UserTaskService(
        MatchUserTaskService matchUserTaskService,
        IUserTaskRepo userTaskRepo)
    {
        _matchUserTaskService = matchUserTaskService;
        _userTaskRepo = userTaskRepo;
    }

    public async Task<R<IEnumerable<QueryUserTaskDto>>> Query(QueryUserTaskReq req)
    {
        var result = await _userTaskRepo.Query(req);
        return result;
    }

    public async Task<R> Claim(CliamUserTaskReq req)
    {
        var userTask = await UserTaskDo.Claim(req);
        return await _userTaskRepo.Save(userTask);
    }
    public async Task<R> Match(MatchUserTaskReq req)
    {
        return await _matchUserTaskService.Match(req);
    }
}
