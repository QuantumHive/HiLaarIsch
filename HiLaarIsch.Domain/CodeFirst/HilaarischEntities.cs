using System.Data.Entity;
using HiLaarIsch.Domain.CodeFirst.Configuration;

namespace HiLaarIsch.Domain
{
    public partial class HiLaarischEntities : DbContext
    {
        public HiLaarischEntities(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<ClassEntity> Classes { get; set; }
        public DbSet<HorseEntity> Horses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(CustomerConfiguration.Instance);
            modelBuilder.Configurations.Add(CustomerClassConfiguration.Instance);
            modelBuilder.Configurations.Add(HorseConfiguration.Instance);

            base.OnModelCreating(modelBuilder);
        }
    }
}
