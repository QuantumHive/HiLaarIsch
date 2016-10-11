using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace HiLaarIsch.Domain
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly HiLaarIschEntities databaseContext;

        public Repository(HiLaarIschEntities databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public IQueryable<TEntity> Entities => this.DbSet;

        public TEntity GetById(Guid id)
        {
            var entity = this.DbSet.Find(id);

            if(entity == null)
            {
                throw new KeyNotFoundException();
            }

            return entity;
        }

        public void Add(TEntity entity)
        {
            this.DbSet.Add(entity);
        }

        private DbSet<TEntity> DbSet => this.databaseContext.Set<TEntity>();
    }
}
