using Shao.ApiTemp.Domain.PromoteTask;
using Shao.ApiTemp.Domain.ThirdOrder;
using Shao.ApiTemp.Domain.UserTask;

namespace Shao.ApiTemp.DomainService.Context;

public class MatchResult
{
    public UserTaskDo UserTask { get; set; }
    public ThirdOrderDo ThirdOrder { get; set; }
    public PromoteTaskSpecDo MatchedSpec { get; set; }
}