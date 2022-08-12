using Shao.ApiTemp.Domain.Dto.PromoteTask;

namespace Shao.ApiTemp.IService;

public interface IPromoteTaskService : IAppService
{
    Task<R<PromoteTaskDto>> Get(PromoteTaskIdReq req);
    Task<R<IEnumerable<QueryPromoteTaskDto>>> Query(QueryPromoteTaskReq req);

    Task<R> Save(SavePromoteTaskReq req);
    Task<R> Publish(PromoteTaskIdReq req);
    Task<R> Close(PromoteTaskIdReq req);
    Task<R> Delete(PromoteTaskIdReq req);
}
