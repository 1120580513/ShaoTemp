namespace Shao.ApiTemp.Repo.Base;

public class DefaultRepo<TCurrentRepo> : BaseRepo
{
    public DefaultRepo() : base(
     App.Config.ConnStr,
     App.CreateLog<TCurrentRepo>())
    {
    }
}
