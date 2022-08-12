using log4net;
using log4net.Core;

namespace Shao.ApiTemp.Common.LogImpl;

public class Log4netCustomLog : ICustomLog
{
    private readonly ILog _log;
    private readonly Type _type;
    public Log4netCustomLog(ILog log, Type type)
    {
        _log = log;
        _type = type;
    }
    public void Trace(string tag, string msg, params object[] args)
    {
        Log(Level.Trace, new LogModel(_type, msg, tag, args));
    }
    public void Info(string tag, string msg, params object[] args)
    {
        Log(Level.Info, new LogModel(_type, msg, tag, args));
    }
    public void Warn(string tag, string msg, params object[] args)
    {
        Log(Level.Warn, new LogModel(_type, msg, tag, args));
    }
    public void Error(string tag, Exception ex, params object[] args)
    {
        Log(Level.Error, new LogModel(_type, ex.Message, tag, args), ex);
    }

    private void Log(Level level, LogModel model, Exception? ex = null)
    {
        _log.Logger.Log(_type, level, model, ex);
    }
}
