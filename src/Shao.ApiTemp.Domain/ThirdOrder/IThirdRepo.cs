using Shao.ApiTemp.Domain.Dto.ThirdOrder;
using Shao.ApiTemp.Domain.UserTask;

namespace Shao.ApiTemp.Domain.ThirdOrder;

public interface IThirdRepo : IRepository
{
    Task<R<IEnumerable<QueryThirdOrderDto>>> QueryByClaimUser(ClaimUser claimUser);
    Task<R<ThirdOrderDo>> GetByUserOrder(UserOrder userOrder);
}