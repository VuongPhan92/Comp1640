using System;
using SimpleInjector;
using Infrastructure.Repository;
using Infrastructure.Caching;
using Infrastructure.Queries;
using Infrastructure.Decorator;
using System.Runtime.Caching;

namespace devPMS.WebCore
{
    public class WebCoreBootstrapper
    {
        private readonly Container _container;

        public WebCoreBootstrapper(Container container)
        {
            _container = container;
        }

        public void Boot()
        {
            // Register the DbContextFactory which is placed in Data also,
            // Please config the connection string whenever you develop or deploy into server
            _container.Register(typeof(IDbContextFactory), typeof(DbContextFactory), Lifestyle.Scoped);
            //_container.RegisterDecorator(typeof(IUnitOfWork), typeof(UnitOfWork<>), Lifestyle.Scoped);
 

            // Go look in all assemblies and register all implementations
            // of ICommandHandler<T> by their closed interface:
            _container.Register(typeof(ICommandHandler<>),
                AppDomain.CurrentDomain.GetAssemblies());

            // Not generic the service for now, abstraction it later on.
            //_container.Register(typeof(IService<>),
            //    AppDomain.CurrentDomain.GetAssemblies());
            //_container.RegisterDecorator(typeof(IService<>),
            //    AppDomain.CurrentDomain.GetAssemblies());

            #region Query
            _container.Register<ObjectCache>(() => new MemoryCache("devPMSCache"), Lifestyle.Singleton);

            // for cach policy
            //_container.Register(typeof(ICachePolicy<>), 
            //    typeof(CachePolicy<>), Lifestyle.Singleton);

            // Start out register all queries without the IQueryProcessor abstraction
            // I can always be added later on without any problem.
            _container.Register(typeof(IQueryHandler<,>),
                AppDomain.CurrentDomain.GetAssemblies());

            _container.RegisterDecorator(typeof(IQueryHandler<,>),
                    typeof(CachingQueryHandlerDecorator<,>));
            #endregion
            // define a simple decorator that validates all query message before they
            // passed to their handlers.
            // using SimpleInjector.Extensions;
            //_container.RegisterDecorator(typeof(IQueryHandler<,>),
            //    typeof(ValidationQueryHandlerDecorator<,>),
            //    context => ShouldQueryHandlerBeValidated(context.ServiceType));

            // Decorate each returned ICommandHandler<T> object with
            // a TransactionCommandHandlerDecorator<T>.
            _container.RegisterDecorator(typeof(ICommandHandler<>),
                typeof(TransactionCommandHandlerDecorator<>));

            // Decorate each returned ICommandHandler<T> object with
            // a DeadlockRetryCommandHandlerDecorator<T>.
            _container.RegisterDecorator(typeof(ICommandHandler<>),
                typeof(DeadlockRetryCommandHandlerDecorator<>));

            // Decorate handlers conditionally with validation. In
            // this case based on their metadata.
            //container.RegisterDecorator(typeof(ICommandHandler<>),
            //    typeof(ValidationCommandHandlerDecorator<>),
            //    context => ContainsValidationAttributes(context.ServiceType));

            // Decorates all handlers with an authorization decorator.
            //container.RegisterDecorator(typeof(ICommandHandler<>),
            //    typeof(AuthorizationCommandHandlerDecorator<>));

            //------
            // The the lifetime scope decorator last (as singleton).
            _container.RegisterDecorator(
                typeof(ICommandHandler<>),
                typeof(LifetimeScopeCommandHandlerDecorator<>),
                Lifestyle.Singleton);

        }

        private object ShouldQueryHandlerBeValidated(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}
