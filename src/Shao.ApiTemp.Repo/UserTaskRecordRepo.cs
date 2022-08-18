using Shao.ApiTemp.Domain.UserTaskRecord;

namespace Shao.ApiTemp.Repo;

public class UserTaskRecordRepo : DefaultConnRepo<UserTaskRecordRepo>, IUserTaskRecordRepo
{
    public async Task<R> Save(UserTaskRecordDo userTaskRecord, UnitOfWork connContext)
    {
        var userTaskRecordPo = App.Map<UserTaskRecordDo, UserTaskRecordPo>(userTaskRecord);
        userTaskRecordPo.ModifyOn = DateTime.Now;

        await InsertOrUpdate(userTaskRecordPo, connContext, "保存用户任务记录失败");
        return R.Succ();
    }
}

