using Shao.ApiTemp.Domain.Dto.PromoteTask;
using Shao.ApiTemp.Domain.Dto.UserTask;
using Shao.ApiTemp.Domain.PromoteTask;
using Shao.ApiTemp.Domain.UserTask;

namespace Shao.ApiTemp.Repo;

public class UserTaskRepo : DefaultConnRepo<UserTaskRepo>, IUserTaskRepo
{
    private readonly IPromoteTaskRepo _promoteTaskRepo;

    public UserTaskRepo(IPromoteTaskRepo promoteTaskRepo) : base()
    {
        _promoteTaskRepo = promoteTaskRepo;
    }

    public async Task<UserTaskDo> Get(IUserTaskId req)
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
WHERE Mobile = @Mobile AND PromoteTaskId = @PromoteTaskId
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
WHERE Mobile = @Mobile AND ClaimOn > @StartTime
";
        var param = new
        {
            Mobile = claimUser.Mobile,
            StartTime = startTime,
        };
        return await QuerySingle<int>(sql, param, nameof(CountClaimed), "领取任务数量查询失败");
    }
    public async Task<R<IEnumerable<QueryUserTaskDto>>> Query(QueryUserTaskReq req)
    {
        var sql = new SqlBuilder(@"
SELECT  {0}*
FROM dbo.UserTask WITH(NOLOCK)
{1}
")
            .Fill[0].Append("{0}")
            .Builder.Fill[1]
            .Where.ParamAnd(nameof(UserTaskPo.PromoteTaskId), req.PromoteTaskId.HasValue)
            .Where.ParamAnd(nameof(UserTaskPo.StoreId), req.StoreId.HasValue)
            .Where.ParamAnd(nameof(UserTaskPo.UserTaskStatus), req.UserTaskStatus.HasValue)
            .Where.ParamAnd(nameof(UserTaskPo.Mobile), req.Mobile.IsNotEmpty())
            .Where.ParamAnd(nameof(UserTaskPo.OrderNo), req.OrderNo.IsNotEmpty())
            .Where.ParamAndStart(nameof(UserTaskPo.ClaimOn), nameof(req.StartTime), req.StartTime.HasValue)
            .Where.ParamAndEnd(nameof(UserTaskPo.ClaimOn), nameof(req.EndTime), req.EndTime.HasValue)
            .Builder.Build();
        return await PageQuery<QueryUserTaskDto>(sql, req, req, "ModifyOn DESC", nameof(Query), "查询用户任务失败");
    }

    public async Task<R> Save(UserTaskDo userTask, UnitOfWork connContext)
    {
        var userTaskPo = App.Map<UserTaskDo, UserTaskPo>(userTask);
        userTaskPo.ModifyOn = DateTime.Now;

        await InsertOrUpdate(userTaskPo, connContext, "保存用户任务失败");
        return R.Succ();
    }

    private async Task<UserTaskDo> ToDo(UserTaskPo po)
    {
        var userTask = App.Map<UserTaskPo, UserTaskDo>(po);
        userTask.PromoteTask = await _promoteTaskRepo.Get(new PromoteTaskIdReq(po.PromoteTaskId));
        return userTask;
    }
}

