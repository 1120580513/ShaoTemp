namespace Shao.ApiTemp.Common.Dto;

public class R
{
    private const int CODE_SUCCESS = 0;
    private const int CODE_FAIL = -1;

    public R() { }
    public R(int code, string? msg, PageR? page, List<ErrorR>? errors)
    {
        Code = code;
        Msg = msg;
        Page = page;
        Errors = errors;
    }

    public int Code { get; protected set; }
    public string? Msg { get; protected set; }
    public PageR? Page { get; protected set; }
    public List<ErrorR>? Errors { get; protected set; }

    [Newtonsoft.Json.JsonIgnore]
    public bool IsSucc => Code == CODE_SUCCESS;

    public static R Cond(bool isSucc, string failMsg)
    {
        return isSucc ? Succ() : Fail(failMsg);
    }
    public static R Succ(string? msg = null, PageR? page = null)
    {
        return new R(CODE_SUCCESS, msg, page, errors: null);
    }
    public static R Fail(string msg, PageR? page = null, List<ErrorR>? errors = null)
    {
        return new R(CODE_FAIL, msg, page, errors);
    }

    public static R<T> Succ<T>(T data, PageR? page = null, string? msg = null)
    {
        return new RImpl<T>(CODE_SUCCESS, msg, data, page, errors: null);
    }
    public static R<T> Fail<T>(string msg, T? data = default, PageR? page = null, List<ErrorR>? errors = null)
    {
        return new RImpl<T>(CODE_FAIL, msg, data, page, errors);
    }
}
public interface R<out T>
{
    public T? Data { get; }
    public int Code { get; }
    public string? Msg { get; }
    public PageR? Page { get; }
    public List<ErrorR>? Errors { get; }
    [Newtonsoft.Json.JsonIgnore]
    public bool IsSucc { get; }
}
public class RImpl<T> : R, R<T>
{
    public RImpl() { }
    public RImpl(int code, string? msg, T? data, PageR? page, List<ErrorR>? errors) : base(code, msg, page, errors)
    {
        Data = data;
    }
    public T? Data { get; set; }
}
public record ErrorR(string Name, string Msg);
