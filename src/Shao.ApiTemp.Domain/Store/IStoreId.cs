namespace Shao.ApiTemp.Domain.Store;

/// <summary>
/// 保证名称统一
/// </summary>
public interface IStoreId
{
    /// <summary>
    /// 店铺标识
    /// </summary>
    public long StoreId { get; set; }
}
