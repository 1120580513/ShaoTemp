#nullable disable

using Dapper.Contrib.Extensions;

namespace Shao.ApiTemp.Repo.PersistentObject;

[Table("StoreConfig")]
/// <summary>
/// 店铺配置 
/// </summary>
public class StoreConfigPo : IPersistant
{
    [Key]
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>Identity PrimaryKey bigint(8)</remarks>
    public long StoreConfigId {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>bigint(8)</remarks>
    public long StoreId {get;set;} 
    
    /// <summary>
    /// 购买限制天数 
    /// </summary>
    ///<remarks>int(4)</remarks>
    public int PromoteLimitOfDay {get;set;} 
    
    /// <summary>
    /// 购买限制次数 
    /// </summary>
    ///<remarks>int(4)</remarks>
    public int PromoteLimitCount {get;set;} 

    public bool IsInsert() => StoreConfigId == default;
}
