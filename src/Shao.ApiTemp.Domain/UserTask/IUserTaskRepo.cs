using Shao.ApiTemp.Domain.Dto.UserTask;
using Shao.ApiTemp.Domain.PromoteTask;

namespace Shao.ApiTemp.Domain.UserTask;

public interface IUserTaskRepo : IRepository<UserTaskDo>
{
    Task<R<IEnumerable<QueryUserTaskDto>>> Query(QueryUserTaskReq req);
    Task<UserTaskDo> Get(IUserTaskId req);
    /// <summary>
    /// <paramref name="claimUser"/> 是否已领取 <paramref name="promoteTaskId"/> 的推广任务
    /// </summary>
    /// <param name="claimUser"></param>
    /// <param name="promoteTaskId"></param>
    /// <returns></returns>
    Task<bool> HasByPromoteTaskAndUser(ClaimUser claimUser, IPromoteTaskId promoteTaskId);
    /// <summary>
    /// 统计从 <paramref name="startTime"/> 开始 <paramref name="claimUser"/> 领取了多少次任务
    /// </summary>
    /// <param name="claimUser"></param>
    /// <returns></returns>
    Task<int> CountClaimed(ClaimUser claimUser, DateTime startTime);
}

