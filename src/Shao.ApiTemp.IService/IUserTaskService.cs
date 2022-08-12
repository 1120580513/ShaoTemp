using Shao.ApiTemp.Domain.Dto.UserTask;

namespace Shao.ApiTemp.IService;

public interface IUserTaskService : IAppService
{
    Task<R<IEnumerable<QueryUserTaskDto>>> Query(QueryUserTaskReq req);

    Task<R> Claim(CliamUserTaskReq req);
}
