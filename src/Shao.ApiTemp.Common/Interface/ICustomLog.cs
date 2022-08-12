namespace Shao.ApiTemp.Common.Interface;

public interface ICustomLog
{
    void Trace(string tag, string msg, params object[] args);
    void Info(string tag, string msg, params object[] args);
    void Warn(string tag, string msg, params object[] args);
    void Error(string tag, Exception ex, params object[] args);
}
public class LogModel
{
    public LogModel(Type type,string msg, string tag, object[] args)
    {
        this.msg = msg;
        this.tag = $"{type.FullName}.{tag}";
        this.args = args;
    }

    public string msg { get; set; }
    public string tag { get; set; }
    public object[] args { get; set; }
}
