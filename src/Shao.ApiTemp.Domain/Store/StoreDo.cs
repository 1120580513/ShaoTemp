using Shao.ApiTemp.Domain.Dto.Store;

namespace Shao.ApiTemp.Domain.Store;

/// <summary>
/// 店铺 
/// </summary>
public class StoreDo : BaseDo, IStoreId
{
    /// <inheritdoc />
    public long StoreId { get; set; }
    /// <summary>
    /// 店铺名称 
    /// </summary>
    public string StoreName { get; set; }
    /// <summary>
    /// 审核限额 
    /// </summary>
    public decimal AuditQuota { get; set; }
    /// <summary>
    /// 店铺状态
    /// </summary>
    public StoreStatus StoreStatus { get; set; }
    /// <summary>
    ///  
    /// </summary>
    public DateTime CreateOn { get; set; }

    /// <summary>
    /// 店铺配置
    /// </summary>
    public StoreConfigDo? Config { get; set; }

    /// <summary>
    /// 自动映射使用
    /// </summary>
    public StoreDo() { }
    public static async Task<StoreDo> Create(SaveStoreReq req)
    {
        var store = App.Map<SaveStoreReq, StoreDo>(req);
        store.StoreId = default;
        store.StoreStatus = StoreStatus.Off;
        store.CreateOn = DateTime.Now;
        await store.EnsureStoreNameUnique();
        return store;
    }
    public async Task Update(SaveStoreReq req)
    {
        AuditQuota = req.AuditQuota;
        StoreName = req.StoreName!;

        if (StoreName != req.StoreName)
        {
            await EnsureStoreNameUnique();
        }
    }
    public void Open()
    {
        AreEnsure(StoreStatus == StoreStatus.Off, "已开启，请刷新页面", StoreId, StoreStatus);
        StoreStatus = StoreStatus.On;
    }
    public void Close()
    {
        AreEnsure(StoreStatus == StoreStatus.On, "已关闭，请刷新页面", StoreId, StoreStatus);
        StoreStatus = StoreStatus.Off;
    }
    public void UpdateConfig(SaveStoreConfigReq req)
    {
        StoreConfigDo config = App.Map<SaveStoreConfigReq, StoreConfigDo>(req!);
        config.StoreConfigId = Config?.StoreConfigId ?? default;
        Config = config;
    }
    public void EnsureCanUse()
    {
        AreEnsure(StoreStatus == StoreStatus.On, $"{StoreName} 未开启", StoreId, StoreName, StoreStatus);
    }

    private async Task EnsureStoreNameUnique()
    {
        var store = await _repo.GetByStoreName(StoreName);
        AreEnsure(store is null, $"{StoreName} 已存在", StoreId, StoreStatus);
    }

    private readonly IStoreRepo _repo = new Lazy<IStoreRepo>(() => App.Resolve<IStoreRepo>()).Value;
}
