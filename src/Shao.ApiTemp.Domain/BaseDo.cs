namespace Shao.ApiTemp.Domain;

public class BaseDo : IEntity
{
    /// <inheritdoc />
    /// <exception cref="DomainException"/>
    public virtual void AreEnsure(bool condition, string message, params object[] args)
    {
        if (!condition) throw new DomainException(message, args);
    }
}
