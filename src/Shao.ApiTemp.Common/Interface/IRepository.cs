namespace Shao.ApiTemp.Common.Interface;

public interface IRepository { }

public interface IRepository<TDomainObject> : IRepository
{
    public async Task<R> Save(TDomainObject domainObject)
    {
        return await Save(domainObject, UnitOfWork.Default);
    }

    public Task<R> Save(TDomainObject domainObject, UnitOfWork unitOfWork);
}