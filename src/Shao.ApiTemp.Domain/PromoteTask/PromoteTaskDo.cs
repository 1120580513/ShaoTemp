using Shao.ApiTemp.Domain.Dto.PromoteTask;
using Shao.ApiTemp.Domain.Dto.Store;
using Shao.ApiTemp.Domain.Store;
using Shao.ApiTemp.Domain.UserTask;

namespace Shao.ApiTemp.Domain.PromoteTask;

/// <summary>
/// 推广任务 
/// </summary>
public class PromoteTaskDo : BaseDo, IPromoteTaskId
{
    /// <summary>
    ///  
    /// </summary>
    public long PromoteTaskId { get; set; }
    public StoreDo Store { get; set; }
    /// <summary>
    /// 推广任务名称 
    /// </summary>
    public string PromoteTaskName { get; set; }
    /// <summary>
    /// 推广任务状态 
    /// </summary>
    public PromoteTaskStatus PromoteTaskStatus { get; set; }
    /// <summary>
    /// 推广任务开始时间 
    /// </summary>
    public DateTime StartTime { get; set; }
    /// <summary>
    /// 推广任务结束时间 
    /// </summary>
    public DateTime EndTime { get; set; }
    /// <summary>
    ///  
    /// </summary>
    public DateTime CreateOn { get; set; }
    /// <summary>
    /// 任务规格
    /// </summary>
    public List<PromoteTaskSpecDo> Specs { get; set; }

    /// <summary>
    /// 自动映射使用
    /// </summary>
    public PromoteTaskDo() { }
    public static async Task<PromoteTaskDo> Create(SavePromoteTaskReq req)
    {
        var promoteTask = App.Map<SavePromoteTaskReq, PromoteTaskDo>(req);
        promoteTask.PromoteTaskId = default;
        promoteTask.PromoteTaskStatus = PromoteTaskStatus.Unpublished;
        promoteTask.CreateOn = DateTime.Now;
        promoteTask.Specs = App.MapList<SavePromoteTaskSpecReq, PromoteTaskSpecDo>(req.Specs).ToList();
        promoteTask.Specs.ForEach(x => x.PromoteTaskSpecId = default);
        await promoteTask.LoadStore(new StoreIdReq(req.StoreId));
        return promoteTask;
    }
    public async Task Save(SavePromoteTaskReq req)
    {
        var canSave = PromoteTaskStatus == PromoteTaskStatus.Unpublished
            || PromoteTaskStatus == PromoteTaskStatus.Published;
        AreEnsure(canSave, $"{PromoteTaskStatus.GetDisplayFormat()} 不能修改", PromoteTaskId, PromoteTaskStatus);

        PromoteTaskName = req.PromoteTaskName;
        StartTime = req.StartTime;
        EndTime = req.EndTime;
        foreach (SavePromoteTaskSpecReq spec in req.Specs)
        {
            if (spec.IsInsert)
            {
                Specs.Add(App.Map<SavePromoteTaskSpecReq, PromoteTaskSpecDo>(spec));
                continue;
            }

            var currentSpec = Specs.FirstOrDefault(x => x.PromoteTaskSpecId == spec.PromoteTaskSpecId);
            AreEnsure(currentSpec is not null, "推广任务规格标识不存在，请刷新页面后重试", req.PromoteTaskId, spec);
            if (spec.IsDelete)
            {
                currentSpec!.FlagDelete();
                continue;
            }

            currentSpec.SpecNum = spec!.SpecNum;
            currentSpec.GiveGoodsId = spec!.GiveGoodsId;
            currentSpec.GiveGoodsName = spec!.GiveGoodsName;
            currentSpec.GiveGoodsCode = spec!.GiveGoodsCode;
            currentSpec.GiveGoodsNum = spec!.GiveGoodsNum;
        }

        if(Store.StoreId != req.StoreId)
        {
            await LoadStore(new StoreIdReq(req.StoreId));
        }
    }
    public void Publish()
    {
        AreEnsure(PromoteTaskStatus == PromoteTaskStatus.Unpublished,
            $"{PromoteTaskStatus.GetDisplayFormat()} 不能发布", PromoteTaskId, PromoteTaskStatus);
        PromoteTaskStatus = PromoteTaskStatus.Published;
    }
    public void Close()
    {
        AreEnsure(PromoteTaskStatus == PromoteTaskStatus.Published,
            $"{PromoteTaskStatus.GetDisplayFormat()} 不能终止", PromoteTaskId, PromoteTaskStatus);
        PromoteTaskStatus = PromoteTaskStatus.Closed;
    }
    public void Delete()
    {
        AreEnsure(PromoteTaskStatus == PromoteTaskStatus.Unpublished
            || PromoteTaskStatus == PromoteTaskStatus.Closed,
            $"{PromoteTaskStatus.GetDisplayFormat()} 不能删除", PromoteTaskId, PromoteTaskStatus);
        PromoteTaskStatus = PromoteTaskStatus.Deleted;
    }

    public void EnsureCanClaim(ClaimUser claimUser)
    {
        AreEnsure(PromoteTaskStatus == PromoteTaskStatus.Published,
            $"{PromoteTaskName} {PromoteTaskStatus.GetDisplayFormat()} 不可领取", PromoteTaskId, PromoteTaskStatus);
    }

    private async Task LoadStore(IStoreId storeId)
    {
        Store = await _storeRepo.Get(new StoreIdReq(storeId.StoreId));
        Store.EnsureCanUse();
    }

    private readonly IPromoteTaskRepo _repo = new Lazy<IPromoteTaskRepo>(() => App.Resolve<IPromoteTaskRepo>()).Value;
    private readonly IStoreRepo _storeRepo = new Lazy<IStoreRepo>(() => App.Resolve<IStoreRepo>()).Value;
}
