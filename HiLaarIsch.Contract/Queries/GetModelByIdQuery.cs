using System;
using QuantumHive.Core;

namespace HiLaarIsch.Contract.Queries
{
    public class GetModelByIdQuery<TModel> : IQuery<TModel>
        where TModel : class, new()
    {
        public GetModelByIdQuery(Guid id, bool throwIfNotExists = true)
        {
            this.Id = id;
            this.ThrowIfNotExistis = throwIfNotExists;
        }

        public Guid Id { get; }
        public bool ThrowIfNotExistis { get; }
    }
}
