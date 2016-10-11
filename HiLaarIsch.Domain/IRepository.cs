using System;
using System.Linq;

namespace HiLaarIsch.Domain
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Entities { get; }
        TEntity GetById(Guid id);
        void Add(TEntity entity);
    }
}
