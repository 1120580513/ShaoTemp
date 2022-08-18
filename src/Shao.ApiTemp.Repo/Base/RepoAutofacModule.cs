using Autofac;

namespace Shao.ApiTemp.Repo.Base
{
    public class RepoAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWorkFactory>().As<IUnitOfWorkFactory>().SingleInstance();
        }
    }
    public class UnitOfWorkFactory : DefaultConnRepo<UnitOfWorkFactory> { }
}