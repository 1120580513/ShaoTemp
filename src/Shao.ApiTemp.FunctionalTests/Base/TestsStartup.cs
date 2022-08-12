using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shao.ApiTemp.FunctionalTests.Base;

public class TestsStartup : Startup
{
    public TestsStartup(IConfiguration configuration) : base(configuration)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.Configure<RouteOptions>(Configuration);

        base.ConfigureServices(services);
    }
}
