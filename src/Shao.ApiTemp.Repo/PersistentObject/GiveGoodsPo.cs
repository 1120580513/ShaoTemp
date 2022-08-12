#nullable disable

using Dapper.Contrib.Extensions;

namespace Shao.ApiTemp.Repo.PersistentObject;

[Table("GiveGoods")]
/// <summary>
/// 赠品 
/// </summary>
public class GiveGoodsPo : IPersistant
{
    [Key]
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>Identity PrimaryKey bigint(8)</remarks>
    public long GiveGoodsId {get;set;} 
    
    /// <summary>
    /// 赠品名称 
    /// </summary>
    ///<remarks>nvarchar(256)</remarks>
    public string GiveGoodsName {get;set;} 
    
    /// <summary>
    /// 赠品编码 
    /// </summary>
    ///<remarks>varchar(32)</remarks>
    public string GiveGoodsCode {get;set;} 
    
    /// <summary>
    /// 赠送数量 
    /// </summary>
    ///<remarks>int(4)</remarks>
    public int GiveGoodsNum {get;set;} 
    
    /// <summary>
    /// 赠品状态 
    /// </summary>
    ///<remarks>int(4)</remarks>
    public int GiveGoodsStatus {get;set;} 
    
    /// <summary>
    ///  
    /// </summary>
    ///<remarks>datetime(8)[3]</remarks>
    public DateTime CreateOn {get;set;} 

    public bool IsInsert() => GiveGoodsId == default;
}
