using Shao.ApiTemp.Domain.Dto.PromoteTask;

namespace Shao.ApiTemp.Domain.PromoteTask;

public interface IPromoteTaskRepo : IRepository
{
    Task<R<IEnumerable<QueryPromoteTaskDto>>> Query(QueryPromoteTaskReq req);
    Task<PromoteTaskDo> Get(PromoteTaskIdReq req);
    Task<PromoteTaskDo> Get(IPromoteTaskSpecId specId);

    Task<R> Save(PromoteTaskDo promoteTask);
}

