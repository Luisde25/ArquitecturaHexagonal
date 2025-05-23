using Autofac;
using MediatR;
using MediatR.Pipeline;
using System.Reflection;

namespace AdminLibrary.Admin.Infrastructure
{
    public class DefaultInfrastructureModule : Autofac.Module
    {
        private readonly bool _isDevelopment = false;
        private readonly List<Assembly> _assemblies = new List<Assembly>(); 
        public delegate object ServiceFactory(Type serviceType);



        protected override void Load(ContainerBuilder builder)
        {
            if (_isDevelopment)
            {
                RegisterDeveloomentOnlyDependecnies(builder);
            }

            RegisterCommonDependecies(builder);
        }

        private void RegisterCommonDependecies(ContainerBuilder builder)
        {
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();

                return t => c.Resolve(t);
            });

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(IRequestExceptionHandler<,,>),
                typeof(IRequestExceptionAction<,>),
                typeof(INotificationHandler<>),
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder.RegisterAssemblyTypes(_assemblies.ToArray())
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }
        }


        private void RegisterDeveloomentOnlyDependecnies(ContainerBuilder builder)
        {

        }
    }
}
