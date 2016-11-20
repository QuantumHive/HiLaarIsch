using QuantumHive.Core;

namespace HiLaarIsch.Contract.Queries
{
    public class GetModelByIdQuery<TModel> : IQuery<TModel>
        where TModel : class, new()
    {
        public GetModelByIdQuery(int id, bool throwIfNotExists = true)
        {
            this.Id = id;
            this.ThrowIfNotExistis = throwIfNotExists;
        }

        public int Id { get; }
        public bool ThrowIfNotExistis { get; }
    }
}
