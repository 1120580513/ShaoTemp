using System.Text;

namespace Microsoft.AspNetCore.Builder;

public static class HttpLogMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomHttpLog(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<HttpLogMiddleware>();

        return applicationBuilder;
    }
}
public class HttpLogMiddleware
{
    private readonly RequestDelegate _next;
    public HttpLogMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var log = App.CreateLog<HttpLogMiddleware>();

        var builder = new StringBuilder();
        await GenRawRequestMessage(context, builder);

        var originResponseStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next.Invoke(context);

        responseBody.Seek(0, SeekOrigin.Begin);
        var responseBodyStr = await new StreamReader(responseBody).ReadToEndAsync();
        responseBody.Seek(0, SeekOrigin.Begin);
        await responseBody.CopyToAsync(originResponseStream);

        GenRawResponseMessage(context, builder, responseBodyStr);

        var rawHttpMessage = builder.ToString();
        log.Trace(nameof(Invoke), "HTTP Message Record", rawHttpMessage);
    }

    private async Task<StringBuilder> GenRawRequestMessage(HttpContext context, StringBuilder builder)
    {
        var request = context.Request;

        builder.AppendLine($"{request.Method} {request.Scheme}://{request.Host}{request.Path}{request.QueryString} {request.Protocol}");
        foreach (var header in request.Headers)
        {
            builder.AppendLine($"{header.Key}: {header.Value}");
        }
        builder.AppendLine();

        request.EnableBuffering();
        var streamReader = new StreamReader(request.Body);
        var body = await streamReader.ReadToEndAsync();
        request.Body.Seek(0, SeekOrigin.Begin);
        builder.AppendLine(body);

        return builder;
    }
    private StringBuilder GenRawResponseMessage(HttpContext context, StringBuilder builder, string responseBody)
    {
        builder.AppendLine();
        builder.AppendLine();

        var response = context.Response;

        builder.AppendLine($"{context.Request.Protocol} {response.StatusCode}");
        foreach (var header in response.Headers)
        {
            builder.AppendLine($"{header.Key}: {header.Value}");
        }
        builder.AppendLine();
        builder.AppendLine(responseBody);

        return builder;
    }
}
