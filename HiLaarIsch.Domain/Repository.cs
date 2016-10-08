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

        private DbSet<TEntity> DbSet => this.databaseContext.Set<TEntity>();
    }
}
