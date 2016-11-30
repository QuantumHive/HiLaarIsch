using System.Linq;

namespace HiLaarIsch.Domain
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Entities { get; }
        TEntity GetById(int id);
        void Add(TEntity entity);
    }
}
