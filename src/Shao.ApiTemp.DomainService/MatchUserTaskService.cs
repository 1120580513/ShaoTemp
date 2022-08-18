using Shao.ApiTemp.Domain.Dto.UserTask;
using Shao.ApiTemp.Domain.ThirdOrder;
using Shao.ApiTemp.Domain.UserTask;
using Shao.ApiTemp.Domain.UserTaskRecord;
using Shao.ApiTemp.DomainService.Context;

namespace Shao.ApiTemp.DomainService;

public class MatchUserTaskService : BaseService
{
    private readonly IUserTaskRepo _userTaskRepo;
    private readonly IUserTaskRecordRepo _userTaskRecordRepo;
    private readonly IThirdRepo _thirdRepo;

    public MatchUserTaskService(
        IUserTaskRepo userTaskRepo,
        IUserTaskRecordRepo userTaskRecordRepo,
        IThirdRepo thirdRepo)
    {
        _userTaskRepo = userTaskRepo;
        _thirdRepo = thirdRepo;
        _userTaskRecordRepo = userTaskRecordRepo; 
    }

    //public async Task<R> ExecuteMatch()
    //{

    //}
    public async Task<R> Match(MatchUserTaskReq req)
    {
        var matchR =  await Match(new UserTaskIdReq(req.UserTaskId), new UserOrder() { OrderNo = req.OrderNo });
        return  await SaveMatchResult(matchR.Data!);
    }
    public async Task<R<MatchResult>> Match(IUserTaskId userTaskId, UserOrder userOrder)
    {
        var userTask = await _userTaskRepo.Get(userTaskId);
        userTask.EnsureCanMatch();

        var orderR = await _thirdRepo.GetByUserOrder(userOrder);
        AreEnsure(orderR.IsSucc, "未找到该订单", userOrder);
        ThirdOrderDo order = orderR.Data!;

        var matchSpec = userTask.PromoteTask.Specs
            .FirstOrDefault(x => order.Items.Any(item => item.Num == x.SpecNum));
        AreEnsure(matchSpec is not null, "规格未匹配", userTask, order);

        var result = new MatchResult()
        {
            UserTask = userTask,
            ThirdOrder = order,
            MatchedSpec = matchSpec!,
        };
        return R.Succ(result);
    }
    public async Task<R> SaveMatchResult(MatchResult matchResult)
    {
        var userTask = matchResult.UserTask;
        var order = matchResult.ThirdOrder;

        var needAudit = order.PayAmount > userTask.PromoteTask.Store.AuditQuota;
        if (needAudit)
        {
            userTask.SetToWaitAudit();
        }
        else
        {
            userTask.SetToWaitRefund();
        }
        var userTaskRecord = await UserTaskRecordDo.Create(userTask, order, matchResult.MatchedSpec);

        await App.ExecUnitOfWork(async context =>
        {
            await _userTaskRepo.Save(userTask, context);
            await _userTaskRecordRepo.Save(userTaskRecord, context);
        });
        return R.Succ();
    }
}