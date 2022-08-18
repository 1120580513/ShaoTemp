using Shao.ApiTemp.Common.Dto;
using Shao.ApiTemp.Domain.Dto.ThirdOrder;
using Shao.ApiTemp.Domain.ThirdOrder;
using Shao.ApiTemp.Domain.UserTask;

namespace Shao.ApiTemp.RepoRemote;

public class ThirdRepo : IThirdRepo
{
    public async Task<R<ThirdOrderDo>> GetByUserOrder(UserOrder userOrder)
    {
        var thirdOrder = new ThirdOrderDo()
        {
            OrderNo = userOrder.OrderNo,
            Status = ThirdOrderStatus.TradeFinished,
            PayAmount = 1000,
            Items = new List<ThirdOrderItemDo>()
            {
                new ThirdOrderItemDo()
                {
                    Code ="FakeCode",
                    Num = 1,
                },
            }
        };
        return await Task.FromResult(R.Succ(thirdOrder));
    }

    public async Task<R<IEnumerable<QueryThirdOrderDto>>> QueryByClaimUser(ClaimUser claimUser)
    {
        var thirdOrderDto = new QueryThirdOrderDto()
        {
            OrderNo = "FakeOrderNo",
            Status = ThirdOrderStatus.TradeFinished,
        };
        var data = new List<QueryThirdOrderDto>() { thirdOrderDto };
        return await Task.FromResult(R.Succ(data));
    }
}