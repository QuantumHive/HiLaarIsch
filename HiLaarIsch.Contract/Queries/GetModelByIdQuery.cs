using System;
using QuantumHive.Core;

namespace HiLaarIsch.Contract.Queries
{
    public class GetModelByIdQuery<TModel> : IQuery<TModel>
        where TModel : class, new()
    {
        public GetModelByIdQuery(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}
