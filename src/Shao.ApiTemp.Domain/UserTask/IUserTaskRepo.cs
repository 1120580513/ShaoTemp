using Shao.ApiTemp.Domain.Dto.UserTask;
using Shao.ApiTemp.Domain.PromoteTask;

namespace Shao.ApiTemp.Domain.UserTask;

public interface IUserTaskRepo : IRepository<UserTaskDo>
{
    Task<R<IEnumerable<QueryUserTaskDto>>> Query(QueryUserTaskReq req);
    Task<UserTaskDo> Get(IUserTaskId req);
    /// <summary>
    /// <paramref name="claimUser"/> �Ƿ�����ȡ <paramref name="promoteTaskId"/> ���ƹ�����
    /// </summary>
    /// <param name="claimUser"></param>
    /// <param name="promoteTaskId"></param>
    /// <returns></returns>
    Task<bool> HasByPromoteTaskAndUser(ClaimUser claimUser, IPromoteTaskId promoteTaskId);
    /// <summary>
    /// ͳ�ƴ� <paramref name="startTime"/> ��ʼ <paramref name="claimUser"/> ��ȡ�˶��ٴ�����
    /// </summary>
    /// <param name="claimUser"></param>
    /// <returns></returns>
    Task<int> CountClaimed(ClaimUser claimUser, DateTime startTime);
}

