using Microsoft.OpenApi.Any;
using Shao.ApiTemp.Domain;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shao.ApiTemp.Infrastructure.SwaggerGen;

/// <summary>
/// swagger文档生成过滤器，用于枚举描述的生成
/// </summary>
public class EnumDocumentFilter : IDocumentFilter
{
    /// <summary>
    /// 实现IDocumentFilter接口的Apply函数
    /// </summary>
    /// <param name="swaggerDoc"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var dict = typeof(BaseDo).Assembly.GetTypes()
             .Where(x => x.IsEnum).ToDictionary(x => x.Name);

        foreach (var item in swaggerDoc.Components.Schemas)
        {
            var property = item.Value;
            var typeName = item.Key;
            if (property is null || property.Enum.Count == default) continue;
            if (!dict.ContainsKey(typeName)) continue;

            Type itemType = dict[typeName];
            var list = property.Enum.Select(x => (OpenApiInteger)x).ToList();
            property.Description += DescribeEnum(itemType, list);
        }
    }

    private const string Placeholder = " ";

    private string DescribeEnum(Type type, List<OpenApiInteger> enums)
    {
        var builder = new StringBuilder();
        builder.AppendLine("<br/><div>");

        for (int i = 0; i < enums.Count; i++)
        {
            var item = enums[i];
            if (type == null) continue;

            AppendFormatEnum(builder, type, item);

            var isLast = i == enums.Count - 1;
            if (!isLast)
            {
                builder.AppendLine("<br/>");
            }
        }

        builder.AppendLine("</div>");
        return builder.ToString();
    }

    private void AppendFormatEnum(StringBuilder builder, Type type, OpenApiInteger openApiInteger)
    {
        var value = Enum.Parse(type, openApiInteger.Value.ToString());
        var name = Enum.GetName(type, value);

        builder.Append(openApiInteger.Value);
        builder.Append(Placeholder);
        builder.Append(name);

        var display = (DisplayAttribute)type.GetField(name!)!
            .GetCustomAttributes(typeof(DisplayAttribute), inherit: false).First();
        if (display is null) return;

        if (!string.IsNullOrWhiteSpace(display.Name))
        {
            builder.Append(Placeholder);
            builder.Append(display.Name);
        }
        if (!string.IsNullOrWhiteSpace(display.Description))
        {
            builder.Append(Placeholder);
            builder.Append(display.Description);
        }
    }
}