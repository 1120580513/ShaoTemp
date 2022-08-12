using Shao.ApiTemp.Domain.Dto.PromoteTask;
using Shao.ApiTemp.Domain.Dto.UserTask;
using Shao.ApiTemp.Domain.PromoteTask;
using Shao.ApiTemp.Domain.Store;

namespace Shao.ApiTemp.Domain.UserTask;

/// <summary>
/// 用户任务 
/// </summary>
public class UserTaskDo : BaseDo, IUserTaskId
{
    /// <summary>
    ///  
    /// </summary>
    public long UserTaskId { get; set; }
    /// <summary>
    /// 用户任务状态
    /// </summary>
    public UserTaskStatus UserTaskStatus { get; set; }
    /// <summary>
    /// 领取时间
    /// </summary>
    public DateTime ClaimOn { get; set; }
    /// <summary>
    /// 匹配时间 
    /// </summary>
    public DateTime? MatchOn { get; set; }

    /// <summary>
    /// 推广任务
    /// </summary>
    public PromoteTaskDo PromoteTask { get; set; }
    /// <summary>
    /// 领取用户
    /// </summary>
    public ClaimUser ClaimUser { get; set; }
    /// <summary>
    /// 用户订单
    /// </summary>
    public UserOrder Order { get; set; }

    /// <summary>
    /// 自动映射使用
    /// </summary>
    public UserTaskDo() { }
    public static async Task<UserTaskDo> Claim(CliamUserTaskReq req)
    {
        var userTask = App.Map<CliamUserTaskReq, UserTaskDo>(req);
        userTask.UserTaskId = default;
        userTask.UserTaskStatus = UserTaskStatus.WaitMatch;
        userTask.ClaimOn = DateTime.Now;

        await userTask.LoadPromoteTask(new PromoteTaskSpecIdReq(req.PromoteTaskSpecId));
        userTask.PromoteTask.EnsureCanClaim(userTask.ClaimUser);
        await userTask.EnsureCanClaim();
        return userTask;
    }

    private async Task EnsureCanClaim()
    {
        var claimed = await _repo.HasByPromoteTaskAndUser(ClaimUser, new PromoteTaskIdReq(PromoteTask.PromoteTaskId));
        AreEnsure(claimed == false, "不可重复领取", PromoteTask.PromoteTaskId, ClaimUser);

        if (PromoteTask.Store.Config is not null)
        {
            StoreConfigDo storeConfig = PromoteTask.Store.Config!;
            var limitStartTime = DateTime.Now.AddDays(storeConfig.PromoteLimitOfDay);
            var countClaimed = await _repo.CountClaimed(ClaimUser, limitStartTime);
            AreEnsure(countClaimed <= storeConfig.PromoteLimitCount, "已超出领取次数限制", storeConfig.StoreConfigId);
        }
    }
    private async Task LoadPromoteTask(IPromoteTaskSpecId promoteTaskId)
    {
        PromoteTask = await _promoteTaskRepo.Get(promoteTaskId);
    }

    private readonly IUserTaskRepo _repo = new Lazy<IUserTaskRepo>(() => App.Resolve<IUserTaskRepo>()).Value;
    private readonly IPromoteTaskRepo _promoteTaskRepo
        = new Lazy<IPromoteTaskRepo>(() => App.Resolve<IPromoteTaskRepo>()).Value;
}
