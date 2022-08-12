using Shao.ApiTemp.Domain.GiveGoods;

namespace Shao.ApiTemp.Domain.PromoteTask;

public class PromoteTaskSpecDo : IGiveGoodsId, IPromoteTaskSpecId
{
    public long PromoteTaskSpecId { get; set; }
    public int SpecNum { get; set; }
    public long GiveGoodsId { get; set; }
    public string GiveGoodsName { get; set; }
    public string GiveGoodsCode { get; set; }
    public int GiveGoodsNum { get; set; }
    public bool IsDelete { get; set; }

    /// <summary>
    /// 自动映射使用
    /// </summary>
    public PromoteTaskSpecDo() { }

    public void FlagDelete()
    {
        IsDelete = true;
    }
}