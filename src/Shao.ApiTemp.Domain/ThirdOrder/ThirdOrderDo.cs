namespace Shao.ApiTemp.Domain.ThirdOrder;

public class ThirdOrderDo
{
    public string OrderNo { get; set; }
    public ThirdOrderStatus Status { get; set; }
    public decimal PayAmount { get; set; }
    public IEnumerable<ThirdOrderItemDo> Items { get; set; }
}
public class ThirdOrderItemDo
{
    public string Code { get; set; }
    public int Num { get; set; }
}
