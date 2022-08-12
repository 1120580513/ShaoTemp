namespace Shao.ApiTemp.Common.Exceptions;

public class RepositoryException : CustomException
{
    public RepositoryException(string msg, params object[] args) : base(msg, args) { }
    public RepositoryException(string msg, Exception ex, params object[] args) : base(msg, ex, args) { }
}
