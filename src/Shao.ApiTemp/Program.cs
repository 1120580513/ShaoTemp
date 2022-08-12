using Autofac.Extensions.DependencyInjection;

Host.CreateDefaultBuilder(args)
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureWebHostDefaults(builder =>
    {
        builder.UseStartup<Startup>();
    })
    .CustomConfigLog()
    .Build()
    .Run();

