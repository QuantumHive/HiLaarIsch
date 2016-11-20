using System.Data.Entity.ModelConfiguration;

namespace HiLaarIsch.Domain.CodeFirst.Configuration
{
    public class CustomerConfiguration : EntityTypeConfiguration<CustomerEntity>
    {
        public CustomerConfiguration()
        {
            this.HasRequired(m => m.User)
                .WithOptional(m => m.Customer)
                .Map(m => m.MapKey("FK_UserId"))
                .WillCascadeOnDelete(false);
        }

        public static CustomerConfiguration Instance => new CustomerConfiguration();
    }
}
