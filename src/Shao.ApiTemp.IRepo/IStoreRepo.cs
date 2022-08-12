using Shao.ApiTemp.Domain.Dto.Store;
using Shao.ApiTemp.Domain.Store;

namespace Shao.ApiTemp.IRepo;

public interface IStoreRepo : IRepository
{
    Task<R<IEnumerable<QueryStoreDto>>> Query(QueryStoreReq req);
    Task<StoreDo> Get(StoreIdReq req);

    Task<R> Save(StoreDo store);
}

