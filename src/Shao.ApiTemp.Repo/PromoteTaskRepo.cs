using Shao.ApiTemp.Domain.Dto.PromoteTask;
using Shao.ApiTemp.Domain.PromoteTask;
using Shao.ApiTemp.Domain.Store;

namespace Shao.ApiTemp.Repo;

public class PromoteTaskRepo : DefaultConnRepo<PromoteTaskRepo>, IPromoteTaskRepo
{
    private readonly IStoreRepo _storeRepo;

    public PromoteTaskRepo(IStoreRepo storeRepo)
    {
        _storeRepo = storeRepo;
    }

    public async Task<PromoteTaskDo> Get(PromoteTaskIdReq req)
    {
        var promoteTaskPo = await GetPersisent<PromoteTaskPo, long>(req.PromoteTaskId, "推广任务不存在");
        var promoteTask = await ToDo(promoteTaskPo);
        return promoteTask;
    }
    public async Task<PromoteTaskDo> Get(IPromoteTaskSpecId specId)
    {
        var promoteTaskSpecPo =
            await GetPersisent<PromoteTaskSpecPo, long>(specId.PromoteTaskSpecId, "推广任务规格不存在");
        var promoteTaskPo = await GetPersisent<PromoteTaskPo, long>(promoteTaskSpecPo.PromoteTaskId, "推广任务不存在");
        var promoteTask = await ToDo(promoteTaskPo, promoteTaskSpecPo);
        return promoteTask;
    }
    public async Task<R<IEnumerable<QueryPromoteTaskDto>>> Query(QueryPromoteTaskReq req)
    {
        var sql = new SqlBuilder(@"
SELECT  {0}*
FROM dbo.PromoteTask WITH(NOLOCK)
{1}
")
            .Fill[0].Append("{0}")
            .Builder.Fill[1]
            .Where.ParamAnd(nameof(PromoteTaskPo.StoreId), req.StoreId.HasValue)
            .ParamAndLike(nameof(PromoteTaskPo.PromoteTaskName), req.PromoteTaskName.IsNotEmpty())
            .ParamAnd(nameof(PromoteTaskPo.PromoteTaskStatus), req.PromoteTaskStatus.HasValue)
            .ParamAndStart(nameof(PromoteTaskPo.CreateOn), nameof(req.StartTime), req.StartTime.HasValue)
            .ParamAndEnd(nameof(PromoteTaskPo.CreateOn), nameof(req.EndTime), req.EndTime.HasValue)
            .Builder.Build();
        return await PageQuery<QueryPromoteTaskDto>(sql, req, req, "ModifyOn DESC", nameof(Query), "查询推广任务失败");
    }

    public async Task<R> Save(PromoteTaskDo promoteTask, UnitOfWork unitOfWork)
    {
        var promoteTaskPo = App.Map<PromoteTaskDo, PromoteTaskPo>(promoteTask);
        promoteTaskPo.ModifyOn = DateTime.Now;

        return await TranTemplate(unitOfWork, async unitOfWork =>
         {
             await InsertOrUpdate(promoteTaskPo, unitOfWork, "保存推广任务失败");
             foreach (var specDo in promoteTask.Specs)
             {
                 var specPo = App.Map<PromoteTaskSpecDo, PromoteTaskSpecPo>(specDo);
                 specPo.PromoteTaskId = promoteTaskPo.PromoteTaskId;
                 await InsertOrUpdateOrDelete(specDo.IsDelete, specPo, unitOfWork, "保存推广任务规格失败");
             }
         }, nameof(Save), "保存推广任务失败", promoteTask, promoteTaskPo);
    }

    private async Task<PromoteTaskDo> ToDo(PromoteTaskPo po)
    {
        var promoteTask = App.Map<PromoteTaskPo, PromoteTaskDo>(po);
        var specSql = @"
SELECT *
FROM dbo.PromoteTaskSpec WITH(NOLOCK)
WHERE PromoteTaskId = @PromoteTaskId
";
        var specs = await Query<PromoteTaskSpecPo>(specSql, po, nameof(ToDo), "查询推广任务规格失败");
        promoteTask.Specs = App.MapList<PromoteTaskSpecPo, PromoteTaskSpecDo>(specs).ToList();
        promoteTask.Store = await _storeRepo.Get(new Domain.Dto.Store.StoreIdReq(po.StoreId));
        return promoteTask;
    }
    private async Task<PromoteTaskDo> ToDo(PromoteTaskPo promoteTaskPo, PromoteTaskSpecPo promoteTaskSpecPo)
    {
        var promoteTask = App.Map<PromoteTaskPo, PromoteTaskDo>(promoteTaskPo);
        promoteTask.Specs = new List<PromoteTaskSpecDo>()
        {
             App.Map<PromoteTaskSpecPo, PromoteTaskSpecDo>(promoteTaskSpecPo)
        };
        promoteTask.Store = await _storeRepo.Get(new Domain.Dto.Store.StoreIdReq(promoteTaskPo.StoreId));
        return promoteTask;
    }
}

