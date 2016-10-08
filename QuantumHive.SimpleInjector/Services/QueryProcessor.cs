﻿using QuantumHive.Core;
using SimpleInjector;

namespace QuantumHive.SimpleInjector.Services
{
    public class QueryProcessor : IQueryProcessor
    {
        private readonly Container container;

        public QueryProcessor(Container container)
        {
            this.container = container;
        }

        public TResult Process<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));

            dynamic handler = container.GetInstance(handlerType);

            return handler.Handle((dynamic)query);
        }
    }
}