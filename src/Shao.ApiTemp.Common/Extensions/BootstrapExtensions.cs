using Autofac;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shao.ApiTemp.Common.AutofacModules;
using System.Globalization;

namespace Microsoft.Extensions.Hosting;

public static class BootstrapExtensions
{
    public static IHostBuilder CustomConfigLog(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureLogging(builder =>
        {
            var fname = $"log4net{(OperatingSystem.IsLinux() ? ".linux" : string.Empty)}.config";
            var path = Path.Combine(Directory.GetCurrentDirectory(), fname);
            var hasLogFile = File.Exists(path);
            Debug.Assert(hasLogFile);
            builder.AddLog4Net(path);
        });

        return hostBuilder;
    }

    public static void AutofacConfigureContainer(this ContainerBuilder builder, params Type[] instances)
    {
        // 强制指定验证语言
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("zh-CN");

        builder.RegisterModule(new ApplicationModule(instances));
    }
}