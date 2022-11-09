using Autofac;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Hosting;
using Shao.ApiTemp.Common.Mq;
using Shao.ApiTemp.Common.Mq.RabbitMq;

namespace Shao.ApiTemp.Common.AutofacModules;

public class ApplicationModule : Module
{
    private readonly Type[] _typeInstances;

    public ApplicationModule(Type[] types)
    {
        _typeInstances = types;
    }

    protected override void Load(ContainerBuilder builder)
    {
        RegisterLog(builder);
        RegisterMq(builder);
        RegisterApplication(builder);
    }

    private void RegisterLog(ContainerBuilder builder)
    {
        log4net.ILog log = log4net.LogManager.GetLogger("root");
        builder.RegisterInstance(log).As<log4net.ILog>();
    }
    private void RegisterMq(ContainerBuilder builder)
    {
        builder.RegisterType<RabbitMqClient>().As<IMqClient>().InstancePerDependency();
    }
    private void RegisterApplication(ContainerBuilder builder)
    {
        if (_typeInstances is null || _typeInstances.Length == default) return;

        var mapperConfig = new MapperConfigurationExpression();
        foreach (Type type in _typeInstances)
        {
            var needMapper = false;
            if (typeof(IAppService).IsAssignableFrom(type))
            {
                builder.RegisterAssemblyTypes(type.Assembly).AsImplementedInterfaces().SingleInstance();
            }
            else if (typeof(IRepository).IsAssignableFrom(type))
            {
                needMapper = true;
                builder.RegisterAssemblyTypes(type.Assembly).AsImplementedInterfaces().SingleInstance();
            }
            else if (typeof(IReq).IsAssignableFrom(type))
            {
                needMapper = true;
                builder.RegisterAssemblyTypes(type.Assembly).Where(x => x.IsClosedTypeOf(typeof(IValidator<>)))
                    .AsImplementedInterfaces()
                    .SingleInstance();
            }
            else if (typeof(IDomainService).IsAssignableFrom(type))
            {
                builder.RegisterAssemblyTypes(type.Assembly).SingleInstance();
            }
            else if (typeof(BackgroundService).IsAssignableFrom(type))
            {
                builder.RegisterAssemblyTypes(type.Assembly).Where(t => t.IsAssignableFrom(typeof(BackgroundService)))
                    .As<IHostedService>().InstancePerDependency();
            }
            else if (typeof(IMqReceived).IsAssignableFrom(type))
            {
                builder.RegisterAssemblyTypes(type.Assembly).As<IMqReceived>().SingleInstance();
            }

            if (needMapper)
            {
                mapperConfig.AddMaps(type.Assembly);
            }
        }

        ConfigrueAutoMapper(mapperConfig);
        builder.RegisterInstance(new MapperConfiguration(mapperConfig).CreateMapper());
    }


    private void ConfigrueAutoMapper(IMapperConfigurationExpression config) { }
}