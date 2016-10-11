using QuantumHive.Core;

namespace HiLaarIsch.Contract.Queries
{
    public class GetAllModelsQuery<TModel> : IQuery<TModel[]>
		where TModel : class, new()
    {
    }
}