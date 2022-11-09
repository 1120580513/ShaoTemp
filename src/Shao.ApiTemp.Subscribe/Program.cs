using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shao.ApiTemp.Service.Base;
using Shao.ApiTemp.Subscribe;
using Shao.ApiTemp.Subscribe.Receiveds;

IConfiguration? configuration = null;
var host = Host.CreateDefaultBuilder(args)
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .CustomConfigLog()
    .ConfigureContainer<Autofac.ContainerBuilder>(builder =>
    {
        Bootstrap.AutofacConfigureContainer(builder, typeof(SaveGiveGoodsReceived), typeof(MqSubscribeWorker));
    })
    .ConfigureServices((context, services) =>
    {
        configuration = context.Configuration;
        services.AddAutofac();
        services.AddHostedService<MqSubscribeWorker>();
    })
    .Build();
Bootstrap.InitApp(host.Services, configuration!);
host.Run();

