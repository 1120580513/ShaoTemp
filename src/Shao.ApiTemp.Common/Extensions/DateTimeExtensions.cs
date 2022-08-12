using Shao.ApiTemp.Common.Constant;

namespace Shao.ApiTemp.Common.Extensions;

public static class DateTimeExtensions
{
    public static string Format(this DateTime dateTime)
    {
        return dateTime.ToString(StringConsts.DateFormat);
    }
}
