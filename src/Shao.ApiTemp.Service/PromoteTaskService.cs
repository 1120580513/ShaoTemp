using Shao.ApiTemp.Domain.Dto.PromoteTask;
using Shao.ApiTemp.Domain.PromoteTask;

namespace Shao.ApiTemp.Service;

public class PromoteTaskService : BaseService, IPromoteTaskService
{
    private readonly IPromoteTaskRepo _promoteTaskRepo;

    public PromoteTaskService(IPromoteTaskRepo promoteTaskRepo)
    {
        _promoteTaskRepo = promoteTaskRepo;
    }

    public async Task<R<PromoteTaskDto>> Get(PromoteTaskIdReq req)
    {
        var promoteTask = await _promoteTaskRepo.Get(req);
        PromoteTaskDto data = App.Map<PromoteTaskDo, PromoteTaskDto>(promoteTask);
        return R.Succ(data);
    }
    public async Task<R<IEnumerable<QueryPromoteTaskDto>>> Query(QueryPromoteTaskReq req)
    {
        var result = await _promoteTaskRepo.Query(req);
        return result;
    }

    public async Task<R> Save(SavePromoteTaskReq req)
    {
        PromoteTaskDo promoteTask;
        if (req.IsInsert())
        {
            promoteTask =  await PromoteTaskDo.Create(req);
        }
        else
        {
            promoteTask = await _promoteTaskRepo.Get(req);
            await promoteTask.Save(req);
        }
        return await _promoteTaskRepo.Save(promoteTask);
    }
    public async Task<R> Publish(PromoteTaskIdReq req)
    {
        var promoteTask = await _promoteTaskRepo.Get(req);
        promoteTask.Publish();
        return await _promoteTaskRepo.Save(promoteTask);
    }
    public async Task<R> Close(PromoteTaskIdReq req)
    {
        var promoteTask = await _promoteTaskRepo.Get(req);
        promoteTask.Close();
        return await _promoteTaskRepo.Save(promoteTask);
    }
    public async Task<R> Delete(PromoteTaskIdReq req)
    {
        var promoteTask = await _promoteTaskRepo.Get(req);
        promoteTask.Delete();
        return await _promoteTaskRepo.Save(promoteTask);
    }
}
