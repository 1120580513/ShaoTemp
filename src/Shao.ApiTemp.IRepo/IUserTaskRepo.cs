using Shao.ApiTemp.Domain.Dto.UserTask;
using Shao.ApiTemp.Domain.UserTask;

namespace Shao.ApiTemp.IRepo;

public interface IUserTaskRepo : IRepository
{
    Task<R<IEnumerable<QueryUserTaskDto>>> Query(QueryUserTaskReq req);
    Task<UserTaskDo> Get(UserTaskIdReq req);

    Task<R> Save(UserTaskDo userTask);
}

