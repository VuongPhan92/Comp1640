using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Decorator
{
    public sealed class LifetimeScopeCommandHandlerDecorator<T>
                    : ICommandHandler<T>
    {
        private readonly Container container;
        private readonly Func<ICommandHandler<T>> decorateeFactory;

        public LifetimeScopeCommandHandlerDecorator(Container container,
            Func<ICommandHandler<T>> decorateeFactory)
        {
            this.decorateeFactory = decorateeFactory;
            this.container = container;
        }

        public void Handle(T command)
        {
            //using (this.container.BeginLifetimeScope())
            //{
                var decoratee = this.decorateeFactory.Invoke();
                decoratee.Handle(command);
            //}
        }
    }
}
