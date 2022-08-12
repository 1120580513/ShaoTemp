namespace Shao.ApiTemp.Common.Exceptions;

public class DomainException : CustomException
{
    public DomainException(string message, params object[] args) : base(message, args)
    {
    }
}
