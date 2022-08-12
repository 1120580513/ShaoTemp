#nullable disable

using Dapper.Contrib.Extensions;

namespace Shao.ApiTemp.Repo.PersistentObject;

[Table("PromoteTaskSpec")]
/// <summary>
///  
/// </summary>
public class PromoteTaskSpecPo : IPersistant
{
    [Key]
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>Identity PrimaryKey bigint(19)</remarks>
    public long PromoteTaskSpecId {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>bigint(19)</remarks>
    public long PromoteTaskId {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>int(10)</remarks>
    public int SpecNum {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>bigint(19)</remarks>
    public long GiveGoodsId {get;set;} 
    
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

    public bool IsInsert() => PromoteTaskSpecId == default;
}
