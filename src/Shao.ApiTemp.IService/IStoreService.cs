using Shao.ApiTemp.Domain.Dto.Store;

namespace Shao.ApiTemp.IService;

public interface IStoreService : IAppService
{
    Task<R<StoreDto>> Get(StoreIdReq req);
    /// <summary>
    /// 获取店铺配置
    /// </summary>
    /// <param name="req"></param>
    /// <returns>可能没有店铺配置</returns>
    Task<R<StoreConfigDto?>> GetConfig(StoreIdReq req);
    Task<R<IEnumerable<QueryStoreDto>>> Query(QueryStoreReq req);

    Task<R> Save(SaveStoreReq req);
    Task<R> Open(StoreIdReq req);
    Task<R> Close(StoreIdReq req);
    Task<R> SaveConfig(SaveStoreConfigReq req);
}
