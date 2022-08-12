using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shao.ApiTemp.Common.Constant;

namespace Shao.ApiTemp.Common.Extensions;

public static class JsonExtensions
{
    public static T FromJson<T>(this string str, JsonSerializerSettings? options = null) where T : class
    {
        return JsonConvert.DeserializeObject<T>(str, options)!;
    }
    public static string ToJson<T>(this T obj, JsonSerializerSettings? options = null) where T : class
    {
        return JsonConvert.SerializeObject(obj, options ?? DefaultSetting.Value);
    }

    public static T? ToObject<T>(
        this string json, T? defVal = default, JsonSerializerSettings? options = null) where T : struct
    {
        if (string.IsNullOrWhiteSpace(json)) return defVal;

        return JsonConvert.DeserializeObject<T>(json, options ?? DefaultSetting.Value);
    }

    private static Lazy<JsonSerializerSettings> DefaultSetting = new Lazy<JsonSerializerSettings>(GetDefaultJsonOptions);

    private static JsonSerializerSettings GetDefaultJsonOptions()
    {
        var setting = new JsonSerializerSettings();
        SetSetting(setting);
        return setting;
    }
    public static void SetSetting(JsonSerializerSettings setting)
    {
        setting.NullValueHandling = NullValueHandling.Ignore;
        setting.DateFormatString = StringConsts.DateFormat;
        setting.ContractResolver = new CamelCasePropertyNamesContractResolver();
    }
}