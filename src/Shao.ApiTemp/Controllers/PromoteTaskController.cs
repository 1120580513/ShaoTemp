using Shao.ApiTemp.Domain.Dto.PromoteTask;

namespace Shao.ApiTemp.Controllers;

public class PromoteTaskController : ApiController
{
    private readonly IPromoteTaskService _promoteTaskService;

    public PromoteTaskController(IPromoteTaskService promoteTaskService)
    {
        _promoteTaskService = promoteTaskService;
    }

    [HttpGet]
    public async Task<R<PromoteTaskDto>> Get([FromQuery] PromoteTaskIdReq req)
    {
        return await _promoteTaskService.Get(req);
    }
    [HttpPost]
    public async Task<R<IEnumerable<QueryPromoteTaskDto>>> Query(QueryPromoteTaskReq req)
    {
        return await _promoteTaskService.Query(req);
    }

    [HttpPost]
    public async Task<R> Save(SavePromoteTaskReq req)
    {
        return await _promoteTaskService.Save(req);
    }
    [HttpPost]
    public async Task<R> Publish(PromoteTaskIdReq req)
    {
        return await _promoteTaskService.Publish(req);
    }
    [HttpPost]
    public async Task<R> Close(PromoteTaskIdReq req)
    {
        return await _promoteTaskService.Close(req);
    }
    [HttpPost]
    public async Task<R> Delete(PromoteTaskIdReq req)
    {
        return await _promoteTaskService.Delete(req);
    }
}
