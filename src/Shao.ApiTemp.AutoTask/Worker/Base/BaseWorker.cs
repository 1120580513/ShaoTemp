using Microsoft.Extensions.Hosting;
using Shao.ApiTemp.Common.Exceptions;
using Shao.ApiTemp.Common.Interface;
using System.Diagnostics;

namespace Shao.ApiTemp.AutoTask.Worker.Base;

public abstract class BaseWorker<TCurrent> : BackgroundService
{
    private readonly TimeSpan _delayTime;
    private readonly ICustomLog _log;

    public BaseWorker(TimeSpan delayTime)
    {
        _delayTime = delayTime;
        _log = App.CreateLog<TCurrent>();
    }

    protected sealed override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(_delayTime);

            try
            {
                await Execute();
            }
            catch (CustomException customEx)
            {
                Debug.Assert(false);
                _log.Error(nameof(ExecuteAsync), customEx, customEx.Args);
            }
            catch (Exception ex)
            {
                Debug.Assert(false);
                _log.Error(nameof(ExecuteAsync), ex);
            }
        }
    }

    protected abstract Task Execute();
}