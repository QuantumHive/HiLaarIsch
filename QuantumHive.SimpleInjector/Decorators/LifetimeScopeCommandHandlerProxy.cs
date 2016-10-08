using System;
using QuantumHive.Core;
using SimpleInjector;

namespace QuantumHive.SimpleInjector.Decorators
{
    public class LifetimeScopeCommandHandlerProxy<T> : ICommandHandler<T>
    {
        private readonly Container container;
        private readonly Func<ICommandHandler<T>> decorateeFactory;

        public LifetimeScopeCommandHandlerProxy(Container container,
            Func<ICommandHandler<T>> decorateeFactory)
        {
            this.container = container;
            this.decorateeFactory = decorateeFactory;
        }

        public void Handle(T command)
        {
            using (container.BeginLifetimeScope())
            {
                ICommandHandler<T> handler = this.decorateeFactory.Invoke();
                handler.Handle(command);
            };
        }
    }
}