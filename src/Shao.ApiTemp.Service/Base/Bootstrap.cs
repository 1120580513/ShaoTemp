using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Shao.ApiTemp.Common.AutofacModules;
using Shao.ApiTemp.Domain.Dto.UserTask;
using Shao.ApiTemp.Domain.Mq;
using Shao.ApiTemp.DomainService;
using Shao.ApiTemp.Repo;
using Shao.ApiTemp.Repo.Base;
using Shao.ApiTemp.RepoRemote;
using System.Globalization;

namespace Shao.ApiTemp.Service.Base;

public static class Bootstrap
{
    public static void AutofacConfigureContainer(Autofac.ContainerBuilder builder, params Type[] otherTypes)
    {
        var types = new List<Type>()
        {
           typeof(UserTaskService),// 注册所有应用服务
           typeof(UserTaskRepo), // 注册所有仓储
           typeof(ThirdRepo), // 注册所有远程仓储
           typeof(QueryUserTaskReq), // 注册所有请求参数的验证
           typeof(MatchUserTaskService),// 注册所有领域服务
        };
        types.AddRange(otherTypes);

        builder.RegisterModule(new ApplicationModule(types.ToArray()));
        builder.RegisterModule(new RepoAutofacModule());
    }

    public static void InitApp(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        // 强制指定验证语言
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("zh-CN");

        App.Init(serviceProvider.GetAutofacRoot(), configuration);
        MqBootstrap.Init();
        App.Mq.Init();
    }
}