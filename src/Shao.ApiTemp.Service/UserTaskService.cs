using Shao.ApiTemp.Domain.Dto.UserTask;
using Shao.ApiTemp.Domain.UserTask;

namespace Shao.ApiTemp.Service;

public class UserTaskService : BaseService, IUserTaskService
{
    private readonly IUserTaskRepo _userTaskRepo;

    public UserTaskService(IUserTaskRepo userTaskRepo)
    {
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
}
