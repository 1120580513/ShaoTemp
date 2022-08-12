using Shao.ApiTemp.Common.Exceptions;
using Shao.ApiTemp.Common.Interface;

namespace Shao.ApiTemp.Service.Base;

public class BaseService : IEnsure
{
    protected TEntity Ensure<TEntity>(R<TEntity> r) where TEntity : IEntity
    {
        AreEnsure(r.IsSucc, r.Msg ?? string.Empty);
        AreEnsure(r.Data is not null, $"{typeof(TEntity).GetDisplayName()} 不未找到");
        return r.Data!;
    }

    public void AreEnsure(bool condition, string message, params object[] args)
    {
        if (!condition) throw new DomainException(message, args);
    }
}
