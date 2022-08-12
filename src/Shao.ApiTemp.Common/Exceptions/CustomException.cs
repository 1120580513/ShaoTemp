namespace Shao.ApiTemp.Common.Exceptions;

public class CustomException : Exception, ICustomException
{
    public CustomException(string msg, params object[] args) : base(msg)
    {
        Args = args;
    }
    public CustomException(string msg, Exception ex, params object[] args) : base(msg, ex)
    {
        Args = args;
    }

    public object[] Args { get; protected set; }
}
