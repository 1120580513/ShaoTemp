namespace Shao.ApiTemp.Common.Exceptions;

public class DomainServiceException : CustomException
{
    public DomainServiceException(string message, params object[] args) : base(message, args)
    {
    }
}