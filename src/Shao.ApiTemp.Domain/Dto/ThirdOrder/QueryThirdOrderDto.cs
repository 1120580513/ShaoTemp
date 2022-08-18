using Shao.ApiTemp.Domain.ThirdOrder;

namespace Shao.ApiTemp.Domain.Dto.ThirdOrder;

public class QueryThirdOrderDto
{
    public string OrderNo { get; set; }
    public ThirdOrderStatus Status { get; set; }
}