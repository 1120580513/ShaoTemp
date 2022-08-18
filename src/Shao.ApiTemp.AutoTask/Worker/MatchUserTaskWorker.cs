using System.Diagnostics;

namespace Shao.ApiTemp.AutoTask.Worker
{
    public class MatchUserTaskWorker : BaseWorker<MatchUserTaskWorker>
    {
        private readonly IUserTaskService _userTaskService;

        public MatchUserTaskWorker(IUserTaskService userTaskService) : base(TimeSpan.FromSeconds(5))
        {
            _userTaskService = userTaskService;
        }

        protected override async Task Execute()
        {
            var r = await _userTaskService.Query(new Domain.Dto.UserTask.QueryUserTaskReq()
            {
                Page = 1,
                PageSize = 10,
            });
            Debug.Assert(false);
        }
    }
}
