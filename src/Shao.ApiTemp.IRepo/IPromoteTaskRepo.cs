using Shao.ApiTemp.Domain.Dto.PromoteTask;
using Shao.ApiTemp.Domain.PromoteTask;

namespace Shao.ApiTemp.IRepo;

public interface IPromoteTaskRepo : IRepository
{
    Task<R<IEnumerable<QueryPromoteTaskDto>>> Query(QueryPromoteTaskReq req);
    Task<PromoteTaskDo> Get(PromoteTaskIdReq req);

    Task<R> Save(PromoteTaskDo promoteTask);
}

