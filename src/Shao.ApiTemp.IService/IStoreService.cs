using Shao.ApiTemp.Domain.Dto.Store;
using Shao.ApiTemp.Domain.Store;

namespace Shao.ApiTemp.IService;

public interface IStoreService : IAppService
{
    Task<R<StoreDto>> Get(IStoreId storeId);
    /// <summary>
    /// 获取店铺配置
    /// </summary>
    /// <param name="storeId"></param>
    /// <returns>可能没有店铺配置</returns>
    Task<R<StoreConfigDto?>> GetConfig(IStoreId storeId);
    Task<R<IEnumerable<QueryStoreDto>>> Query(QueryStoreReq req);

    Task<R> Save(SaveStoreReq req);
    Task<R> Open(IStoreId storeId);
    Task<R> Close(IStoreId storeId);
    Task<R> SaveConfig(SaveStoreConfigReq req);
}
