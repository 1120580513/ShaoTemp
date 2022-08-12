#nullable disable

using Dapper.Contrib.Extensions;

namespace Shao.ApiTemp.Repo.PersistentObject;

[Table("UserTask")]
/// <summary>
/// 用户任务 
/// </summary>
public class UserTaskPo : IPersistant
{
    [Key]
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>Identity PrimaryKey bigint(8)</remarks>
    public long UserTaskId {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>bigint(8)</remarks>
    public long PromoteTaskId {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>nvarchar(128)</remarks>
    public string PromoteTaskName {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>bigint(8)</remarks>
    public long StoreId {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>nvarchar(128)</remarks>
    public string StoreName {get;set;} 
    
    /// <summary>
    /// 用户任务状态 0 已领取 1 待审核 2 待匹配 4 待退款 8 退款失败 16 已完成 
    /// </summary>
    ///<remarks>int(4)</remarks>
    public int UserTaskStatus {get;set;} 
    
    /// <summary>
    /// 用户手机号 
    /// </summary>
    ///<remarks>varchar(11)</remarks>
    public string Mobile {get;set;} 
    
    /// <summary>
    /// 订单号 
    /// </summary>
    ///<remarks>varchar(64)Nullable </remarks>
    public string? OrderNo {get;set;} 
    
    /// <summary>
    /// 匹配时间 
    /// </summary>
    ///<remarks>datetime(8)[3]Nullable </remarks>
    public DateTime? MatchOn {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>datetime(8)[3]</remarks>
    public DateTime CreateOn {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>datetime(8)[3]</remarks>
    public DateTime ModifyOn {get;set;} 

    public bool IsInsert() => UserTaskId == default;
}
