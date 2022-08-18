namespace Shao.ApiTemp.Repo.Base;

public class DefaultConnRepo<TCurrentRepo> : BaseRepo, IUnitOfWorkFactory
{
    public DefaultConnRepo() : base(
     App.Config.ConnStr,
     App.CreateLog<TCurrentRepo>())
    {
    }
}
