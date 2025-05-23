using AdminLibrary.Core.Interfaces;
using AdminLibrary.Core.Services;
using Autofac;

namespace AdminLibrary.Admin.Core
{
    public class DefaultCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HistoryService>().As<IHistoryService>().InstancePerLifetimeScope();
            builder.RegisterType<MaterialService>().As<IMaterialService>().InstancePerLifetimeScope();
            builder.RegisterType<MovementsService>().As<IMovementsService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
        }
    }
}
