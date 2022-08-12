#nullable disable

using Dapper.Contrib.Extensions;

namespace Shao.ApiTemp.Repo.PersistentObject;

[Table("PromoteTask")]
/// <summary>
/// 推广任务 
/// </summary>
public class PromoteTaskPo : IPersistant
{
    [Key]
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>Identity PrimaryKey bigint(8)</remarks>
    public long PromoteTaskId {get;set;} 
    
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
    /// 推广任务名称 
    /// </summary>
    ///<remarks>nvarchar(128)</remarks>
    public string PromoteTaskName {get;set;} 
    
    /// <summary>
    /// 推广任务状态 0 待发布 1 已发布 2 已停止 4 已作废 
    /// </summary>
    ///<remarks>int(4)</remarks>
    public int PromoteTaskStatus {get;set;} 
    
    /// <summary>
    /// 推广任务开始时间 
    /// </summary>
    ///<remarks>datetime(8)[3]</remarks>
    public DateTime StartTime {get;set;} 
    
    /// <summary>
    /// 推广任务结束时间 
    /// </summary>
    ///<remarks>datetime(8)[3]</remarks>
    public DateTime EndTime {get;set;} 
    
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

    public bool IsInsert() => PromoteTaskId == default;
}
