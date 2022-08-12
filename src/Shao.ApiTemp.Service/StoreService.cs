using Shao.ApiTemp.Domain.Dto.Store;
using Shao.ApiTemp.Domain.Store;

namespace Shao.ApiTemp.Service;

public class StoreService : BaseService, IStoreService
{
    private readonly IStoreRepo _storeRepo;

    public StoreService(IStoreRepo storeRepo)
    {
        _storeRepo = storeRepo;
    }

    public async Task<R<StoreDto>> Get(StoreIdReq req)
    {
        var store = await _storeRepo.Get(req);
        var storeDto = App.Map<StoreDo, StoreDto>(store);
        return R.Succ(storeDto);
    }
    /// <inheritdoc />
    public async Task<R<StoreConfigDto?>> GetConfig(StoreIdReq req)
    {
        var store = await _storeRepo.Get(req);
        var storeConfig = App.MapMaybeNull<StoreConfigDo, StoreConfigDto>(store.Config);
        return R.Succ(storeConfig);
    }

    public async Task<R<IEnumerable<QueryStoreDto>>> Query(QueryStoreReq req)
    {
        var queryStoreDtos = await _storeRepo.Query(req);
        return queryStoreDtos;
    }

    public async Task<R> Save(SaveStoreReq req)
    {
        StoreDo store;
        if (req.IsInsert())
        {
            store = await StoreDo.Create(req);
        }
        else
        {
            store = await _storeRepo.Get(req);
            await store.Update(req);
        }
        return await _storeRepo.Save(store);
    }
    public async Task<R> Open(StoreIdReq req)
    {
        var store = await _storeRepo.Get(req);
        store.Open();
        return await _storeRepo.Save(store);
    }
    public async Task<R> Close(StoreIdReq req)
    {
        var store = await _storeRepo.Get(req);
        store.Close();
        return await _storeRepo.Save(store);
    }
    public async Task<R> SaveConfig(SaveStoreConfigReq req)
    {
        var store = await _storeRepo.Get(req);
        store.UpdateConfig(req);
        return await _storeRepo.SaveConfig(store);
    }
}
