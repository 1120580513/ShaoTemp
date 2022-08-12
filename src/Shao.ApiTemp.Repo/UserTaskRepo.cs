using Shao.ApiTemp.Domain.Dto.UserTask;
using Shao.ApiTemp.Domain.PromoteTask;
using Shao.ApiTemp.Domain.UserTask;

namespace Shao.ApiTemp.Repo;

public class UserTaskRepo : DefaultRepo<UserTaskRepo>, IUserTaskRepo
{
    public async Task<UserTaskDo> Get(UserTaskIdReq req)
    {
        var userTaskPo = await GetPersisent<UserTaskPo, long>(req.UserTaskId, "用户任务不存在");
        var userTask = await ToDo(userTaskPo);
        return userTask;
    }
    /// <inheritdoc />
    public async Task<bool> HasByPromoteTaskAndUser(ClaimUser claimUser, IPromoteTaskId promoteTaskId)
    {
        var sql = @"
SELECT 1
FROM dbo.UserTask WITH(NOLOCK)
WHERE Mobile = @Mobile,PromoteTaskId = @PromoteTaskId
";
        var param = new
        {
            Mobile = claimUser.Mobile,
            promoteTaskId.PromoteTaskId,
        };
        var data = await QuerySingle<int?>(sql, param, nameof(HasByPromoteTaskAndUser), "是否领取任务查询失败");
        return data.HasValue && data.Value == 1;
    }
    /// <inheritdoc />
    public async Task<int> CountClaimed(ClaimUser claimUser, DateTime startTime)
    {
        var sql = @"
SELECT COUNT(*)
FROM dbo.UserTask WITH(NOLOCK)
WHERE Mobile = @Mobile,ClaimOn > @StartTime
";
        var param = new
        {
            Mobile = claimUser.Mobile,
            StartTime = startTime,
        };
        return await QuerySingle<int>(sql, param, nameof(CountClaimed), "是否领取任务查询失败");
    }
    public async Task<R<IEnumerable<QueryUserTaskDto>>> Query(QueryUserTaskReq req)
    {
        var sql = @"
SELECT  {0}*
FROM dbo.UserTask WITH(NOLOCK)
";
        return await PageQuery<QueryUserTaskDto>(sql, req, req, "ModifyOn DESC", nameof(Query), "查询用户任务失败");
    }

    public async Task<R> Save(UserTaskDo userTask)
    {
        var userTaskPo = App.Map<UserTaskDo, UserTaskPo>(userTask);
        userTaskPo.ModifyOn = DateTime.Now;

        return await InsertOrUpdate(userTaskPo,"保存用户任务失败");
    }

    private async Task<UserTaskDo> ToDo(UserTaskPo po)
    {
        var userTask = App.Map<UserTaskPo, UserTaskDo>(po);
        return await Task.FromResult(userTask);
    }
}

