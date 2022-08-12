#nullable disable

using Shao.ApiTemp.Domain.GiveGoods;

namespace Shao.ApiTemp.Domain.Dto.GiveGoods;

/// <summary>
///  查询赠品结果项
/// </summary>
public class QueryGiveGoodsDto
{
    /// <summary>
    ///  
    /// </summary>
    public long GiveGoodsId {get;set;} 
    /// <summary>
    /// 赠品名称 
    /// </summary>
    public string GiveGoodsName {get;set;} 
    /// <summary>
    /// 赠品编码 
    /// </summary>
    public string GiveGoodsCode {get;set;} 
    /// <summary>
    /// 赠送数量 
    /// </summary>
    public int GiveGoodsNum {get;set;} 
    /// <summary>
    /// 赠品状态 
    /// </summary>
    public GiveGoodsStatus GiveGoodsStatus {get;set;} 
    /// <summary>
    ///  
    /// </summary>
    public DateTime CreateOn {get;set;} 
}
