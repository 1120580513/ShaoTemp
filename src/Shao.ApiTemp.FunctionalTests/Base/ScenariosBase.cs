using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shao.ApiTemp.Common.Dto;
using Shao.ApiTemp.Common.Extensions;
using System.Reflection;

namespace Shao.ApiTemp.FunctionalTests.Base;

public class ScenariosBase
{
    public TestServer CreateServer()
    {
        var path = Assembly.GetAssembly(typeof(ScenariosBase))
            .Location;

        var webHostBuilder = new WebHostBuilder()
            .UseContentRoot(Path.GetDirectoryName(path))
            .ConfigureAppConfiguration(cb =>
            {
                cb.AddJsonFile("appsettings.json", optional: false).AddEnvironmentVariables();
            })
            .ConfigureServices(cb =>
            {
                cb.AddAutofac();
                // 如果不加以下行，则所有请求均为 404
                cb.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
            }).UseStartup<TestsStartup>();

        var testServer = new TestServer(webHostBuilder);

        return testServer;
    }

    public async Task<R<T>> GetR<T>(HttpClient client, string url)
    {
        var msg = await client.GetAsync(url);
        msg.EnsureSuccessStatusCode();
        var json = await msg.Content.ReadAsStringAsync();
        return json.FromJson<RImpl<T>>();
    }

    public async Task<R> PostR(HttpClient client, string url, Req req)
    {
        var msg = await client.PostAsync(url, BuildContent(req));
        msg.EnsureSuccessStatusCode();
        var json = await msg.Content.ReadAsStringAsync();
        return json.FromJson<R>();
    }
    public async Task<R<T>> PostR<T>(HttpClient client, string url, Req req)
    {
        var msg = await client.PostAsync(url, BuildContent(req));
        msg.EnsureSuccessStatusCode();
        var json = await msg.Content.ReadAsStringAsync();
        return json.FromJson<RImpl<T>>();
    }

    public StringContent BuildContent(object obj)
    {
        return new StringContent(obj.ToJson(), System.Text.Encoding.UTF8, "application/json");
    }
}