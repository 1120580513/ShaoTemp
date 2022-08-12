#nullable disable

using Dapper.Contrib.Extensions;

namespace Shao.ApiTemp.Repo.PersistentObject;

[Table("Store")]
/// <summary>
/// 店铺 
/// </summary>
public class StorePo : IPersistant
{
    [Key]
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>Identity PrimaryKey bigint(19)</remarks>
    public long StoreId {get;set;} 
    
    /// <summary>
    /// 店铺名称 
    /// </summary>
    ///<remarks>nvarchar(64)</remarks>
    public string StoreName {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>int(4)</remarks>
    public int StoreStatus {get;set;} 
    
    /// <summary>
    /// 审核限额 
    /// </summary>
    ///<remarks>decimal(18)[2]</remarks>
    public decimal AuditQuota {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>datetime(23)[3]</remarks>
    public DateTime CreateOn {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>datetime(23)[3]</remarks>
    public DateTime ModifyOn {get;set;} 

    public bool IsInsert() => StoreId == default;
}
