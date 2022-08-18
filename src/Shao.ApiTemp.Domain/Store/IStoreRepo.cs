using Shao.ApiTemp.Domain.Dto.Store;

namespace Shao.ApiTemp.Domain.Store;

public interface IStoreRepo : IRepository
{
    Task<StoreDo> Get(IStoreId req);
    Task<StoreDo?> GetByStoreName(string storeName);
    Task<R<IEnumerable<QueryStoreDto>>> Query(QueryStoreReq req);

    Task<R> Save(StoreDo store);
    Task<R> SaveConfig(StoreDo store);
}

