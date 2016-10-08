using System;
using QuantumHive.Core;
using SimpleInjector;

namespace QuantumHive.SimpleInjector.Decorators
{
    public class LifetimeScopeQueryHandlerProxy<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        private readonly Container container;
        private readonly Func<IQueryHandler<TQuery, TResult>> decorateeFactory;

        public LifetimeScopeQueryHandlerProxy(Container container,
            Func<IQueryHandler<TQuery, TResult>> decorateeFactory)
        {
            this.container = container;
            this.decorateeFactory = decorateeFactory;
        }

        public TResult Handle(TQuery query)
        {
            using (container.BeginLifetimeScope())
            {
                IQueryHandler<TQuery, TResult> handler = this.decorateeFactory.Invoke();
                return handler.Handle(query);
            };
        }
    }
}
