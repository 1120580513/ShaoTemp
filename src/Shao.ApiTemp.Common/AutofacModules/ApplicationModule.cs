﻿using Autofac;
using AutoMapper;
using FluentValidation;

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
        RegisterDdd(builder);
    }

    private void RegisterLog(ContainerBuilder builder)
    {
        log4net.ILog log = log4net.LogManager.GetLogger("root");
        builder.RegisterInstance(log).As<log4net.ILog>();
    }
    private void RegisterDdd(ContainerBuilder builder)
    {
        if (_typeInstances is null || _typeInstances.Length == default) return;

        var mapperConfig = new MapperConfigurationExpression();
        foreach (Type type in _typeInstances)
        {
            if (typeof(IAppService).IsAssignableFrom(type))
            {
                builder.RegisterAssemblyTypes(type.Assembly).As<IAppService>().AsImplementedInterfaces()
                    .SingleInstance();
            }
            else if (typeof(IRepository).IsAssignableFrom(type))
            {
                builder.RegisterAssemblyTypes(type.Assembly).As<IRepository>().AsImplementedInterfaces()
                    .SingleInstance();
            }
            else if (typeof(IReq).IsAssignableFrom(type))
            {
                builder.RegisterAssemblyTypes(type.Assembly).Where(x => x.IsClosedTypeOf(typeof(IValidator<>)))
                    .AsImplementedInterfaces()
                    .SingleInstance();
            }
            mapperConfig.AddMaps(type.Assembly);
        }

        ConfigrueAutoMapper(mapperConfig);
        builder.RegisterInstance(new MapperConfiguration(mapperConfig).CreateMapper());

    }

    private void ConfigrueAutoMapper(IMapperConfigurationExpression config)
    {

    }
}