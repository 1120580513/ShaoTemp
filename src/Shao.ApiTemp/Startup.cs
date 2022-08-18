using Autofac;
using Shao.ApiTemp.DomainService;
using Shao.ApiTemp.Infrastructure.Filters;
using Shao.ApiTemp.Infrastructure.SwaggerGen;
using Shao.ApiTemp.Service.Base;

namespace Shao.ApiTemp;

public class Startup
{
    internal const string CorsName = "CorsPolicy";

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public ILifetimeScope AutofacContainer { get; private set; }

    public virtual void ConfigureServices(IServiceCollection services)
    {
        services
            .AddCustomMvc()
            .AddCustomSwagger(Configuration)
            ;
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        Bootstrap.AutofacConfigureContainer(builder);
    }

    public void Configure(IApplicationBuilder app)
    {
        Bootstrap.InitApp(app.ApplicationServices, Configuration);

        app.CustomUseSwagger();

        app.UseRouting();
        app.UseCors(CorsName);
        app.UseCustomHttpLog();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
        });
    }
}

public static class StartExtensionMethods
{
    public static IServiceCollection AddCustomMvc(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Clear();
            options.Filters.Add(typeof(GlobalExceptionFilter));
            options.Filters.Add(typeof(ActionReqValidFilter));
        })
        .AddNewtonsoftJson(config =>
        {
            JsonExtensions.SetSetting(config.SerializerSettings);
        });

        // 取消模型验证
        services.Configure<ApiBehaviorOptions>(config =>
        {
            config.SuppressModelStateInvalidFilter = true;
        });

        services.AddCors(options =>
        {
            options.AddPolicy(Startup.CorsName,
                builder => builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });

        return services;
    }

    private const string SwaggerName = "v1";

    public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.DocumentFilter<EnumDocumentFilter>();
            options.SwaggerDoc(SwaggerName, new OpenApiInfo
            {
                Title = "Shao.ApiTemp HTTP API",
                Version = "v1",
                Description = ""
            });
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Shao.ApiTemp.xml"));
        });
        return services;
    }

    public static IApplicationBuilder CustomUseSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{SwaggerName}/swagger.json", "Shao.ApiTemp V1");
            });
        return app;
    }
}