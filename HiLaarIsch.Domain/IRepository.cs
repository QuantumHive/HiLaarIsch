using System.Linq;

namespace HiLaarIsch.Domain
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Entities { get; }
    }
}
