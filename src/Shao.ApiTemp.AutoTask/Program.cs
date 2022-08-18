using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shao.ApiTemp.AutoTask.Worker;
using Shao.ApiTemp.Service.Base;

IConfiguration configuration = null;
var host = Host.CreateDefaultBuilder(args)
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .CustomConfigLog()
    .ConfigureContainer<Autofac.ContainerBuilder>(builder =>
    {
        Bootstrap.AutofacConfigureContainer(builder, typeof(MatchUserTaskWorker));
    })
    .ConfigureServices((context, services) =>
    {
        configuration = context.Configuration;
        services.AddAutofac();
        services.AddHostedService<MatchUserTaskWorker>();
    })
    .Build();
Bootstrap.InitApp(host.Services, configuration!);
host.Run();
