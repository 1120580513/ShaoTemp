#nullable disable

using Microsoft.Extensions.Hosting;
using Shao.ApiTemp.Common.Interface;
using Shao.ApiTemp.Common.Mq;

namespace Shao.ApiTemp.Subscribe;

public class MqSubscribeWorker : BackgroundService
{
    private readonly IMqClient _client = App.Mq.Current;
    private readonly ICustomLog _log = App.CreateLog<MqSubscribeWorker>();

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var mqReceiveds = App.Resolve<IEnumerable<IMqReceived>>();
        foreach (var mqReceived in mqReceiveds)
        {
            _client.RegistoryReceived(mqReceived);
        }
        return Task.CompletedTask;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _log.Info(nameof(StartAsync), $"{nameof(MqSubscribeWorker)} Starting ...");
        return base.StartAsync(cancellationToken);
    }
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _log.Info(nameof(StartAsync), $"{nameof(MqSubscribeWorker)} Stoping ...");
        _client.UnLoad();
        await base.StopAsync(cancellationToken);
        _log.Info(nameof(StartAsync), $"{nameof(MqSubscribeWorker)} Stoped ...");
    }
}