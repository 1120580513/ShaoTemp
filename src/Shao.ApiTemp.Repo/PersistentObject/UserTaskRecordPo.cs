#nullable disable

using Dapper.Contrib.Extensions;

namespace Shao.ApiTemp.Repo.PersistentObject;

[Table("UserTaskRecord")]
/// <summary>
///  
/// </summary>
public class UserTaskRecordPo : IPersistant
{
    [Key]
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>Identity PrimaryKey bigint(19)</remarks>
    public long UserTaskRecordId {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>bigint(19)</remarks>
    public long UserTaskId {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>bigint(19)</remarks>
    public long PromoteTaskId {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>bigint(19)</remarks>
    public long PromoteTaskSpecId {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>bigint(19)</remarks>
    public long StoreId {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>bigint(19)</remarks>
    public decimal RefundAmount {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>bigint(19)</remarks>
    public decimal RefundedAmount {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>nvarchar(128)</remarks>
    public string GiveGoodsName {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>varchar(32)</remarks>
    public string GiveGoodsCode {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>int(10)</remarks>
    public int GiveGoodsNum {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>datetime(23)[3]Nullable </remarks>
    public DateTime? CreateOn {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>datetime(23)[3]</remarks>
    public DateTime ModifyOn {get;set;} 

    public bool IsInsert() => UserTaskRecordId == default;
}
