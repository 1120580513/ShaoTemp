using Autofac;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Shao.ApiTemp.Common.LogImpl;
using Shao.ApiTemp.Common.Mq;

namespace Shao.ApiTemp.Common;

public static partial class App
{
    private static ILifetimeScope _service;
    private static IConfiguration _config;
    private static log4net.ILog _log;
    private static IMapper _mapper;
    private static IUnitOfWorkFactory _unitOfWorkFactory;
    private static IMqClient _mqClient;

    public static void Init(
        ILifetimeScope serviceProvider,
        IConfiguration configuration)
    {
        _service = serviceProvider;
        _config = configuration;
        _log = serviceProvider.Resolve<log4net.ILog>();
        _mapper = serviceProvider.Resolve<IMapper>();
        _mqClient = serviceProvider.Resolve<IMqClient>();
        _unitOfWorkFactory = serviceProvider.Resolve<IUnitOfWorkFactory>();
    }

    public static T Resolve<T>() where T : notnull
    {
        return _service.Resolve<T>();
    }

    public static ICustomLog CreateLog<T>()
    {
        return new Log4netCustomLog(_log, typeof(T));
    }

    public static void Publish(MqReq req)
    {
        _mqClient.Publish(req);
    }

    public static TDestination? MapMaybeNull<TSoure, TDestination>(TSoure? source)
        where TSoure : class
        where TDestination : class
    {
        if (source is null) return null;
        return Map<TSoure, TDestination>(source!);
    }
    public static TDestination Map<TSoure, TDestination>(TSoure source)
    {
        return _mapper.Map<TSoure, TDestination>(source);
    }
    public static IEnumerable<TDestination> MapList<TSoure, TDestination>(IEnumerable<TSoure> source)
    {
        return _mapper.Map<IEnumerable<TSoure>, IEnumerable<TDestination>>(source);
    }

    public static async Task ExecUnitOfWork(Func<UnitOfWork, Task> func)
    {
        using var connContext = _unitOfWorkFactory.CreateTranUnitOfWork();
        await func(connContext);
        connContext.Commit();
    }
}