using System.ComponentModel.DataAnnotations;

namespace Shao.ApiTemp.Common.Extensions;

public static class DisplayAttrbuteExtensions
{
    public static string GetDisplayName(this IEntity entity)
    {
        return entity.GetDisplay()?.Name ?? entity.GetType().Name;
    }
    public static DisplayAttribute? GetDisplay(this IEntity entity)
    {
        var type = entity.GetType();
        var displayAttributes = entity.GetType().GetCustomAttributes(typeof(DisplayAttribute), inherit: false);
        if (displayAttributes.Length == default) return null;

        return (DisplayAttribute)displayAttributes[0];
    }

    public static string GetDisplayName(this Type type) 
    {
        return type.GetDisplay()?.Name ?? type.GetType().Name;
    }
    public static DisplayAttribute? GetDisplay(this Type type)
    {
        var displayAttributes = type.GetCustomAttributes(typeof(DisplayAttribute), inherit: false);
        if (displayAttributes.Length == default) return null;

        return (DisplayAttribute)displayAttributes[0];
    }

    public static string GetDisplayFormat(this Enum @enum)
    {
        return $"{GetDisplay(@enum).Name}({@enum.GetHashCode()})";
    }
    public static DisplayAttribute GetDisplay(this Enum @enum)
    {
        var type = @enum.GetType();
        var displayAttributes = type.GetField(Enum.GetName(type, @enum)!)!
            .GetCustomAttributes(typeof(DisplayAttribute), inherit: false);
        if (displayAttributes.Length == default) return new DisplayAttribute() { Name = @enum.ToString() };

        return (DisplayAttribute)displayAttributes[0];
    }
}
