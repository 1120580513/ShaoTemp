using Shao.ApiTemp.Domain.Dto.Store;

namespace Shao.ApiTemp.Controllers;

public class StoreController : ApiController
{
    private readonly IStoreService _storeService;

    public StoreController(IStoreService storeService)
    {
        _storeService = storeService;
    }

    [HttpGet]
    public async Task<R<StoreDto>> Get([FromQuery] StoreIdReq req)
    {
        return await _storeService.Get(req);
    }
    [HttpGet]
    public async Task<R<StoreConfigDto?>> GetConfig([FromQuery] StoreIdReq req)
    {
        return await _storeService.GetConfig(req);
    }
    [HttpPost]
    public async Task<R<IEnumerable<QueryStoreDto>>> Query(QueryStoreReq req)
    {
        return await _storeService.Query(req);
    }

    [HttpPost]
    public async Task<R> Save(SaveStoreReq req)
    {
        return await _storeService.Save(req);
    }
    [HttpPost]
    public async Task<R> SaveConfig(SaveStoreConfigReq req)
    {
        return await _storeService.SaveConfig(req);
    }

    [HttpPost]
    public async Task<R> Open(StoreIdReq req)
    {
        return await _storeService.Open(req);
    }
    [HttpPost]
    public async Task<R> Close(StoreIdReq req)
    {
        return await _storeService.Close(req);
    }
}
