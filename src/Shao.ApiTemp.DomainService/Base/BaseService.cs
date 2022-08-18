namespace Shao.ApiTemp.DomainService.Base;

public class BaseService : IDomainService, IEnsure
{
    public void AreEnsure(bool condition, string message, params object[] args)
    {
        if (!condition) throw new DomainServiceException(message, args);
    }
}