#nullable disable

using Shao.ApiTemp.Domain.Store;

namespace Shao.ApiTemp.Domain.Dto.Store;

/// <summary>
///  店铺
/// </summary>
public class StoreDto
{
    /// <summary>
    ///  
    /// </summary>
    public long StoreId {get;set;} 
    /// <summary>
    /// 店铺名称 
    /// </summary>
    public string StoreName {get;set;} 
    /// <summary>
    /// 审核限额 
    /// </summary>
    public decimal AuditQuota {get;set;}
    /// <summary>
    /// 店铺状态
    /// </summary>
    public StoreStatus StoreStatus { get; set; }
}
