using System.Data.Entity.ModelConfiguration;

namespace HiLaarIsch.Domain.CodeFirst.Configuration
{
    public class CustomerClassConfiguration : EntityTypeConfiguration<CustomerClassAssociation>
    {
        public CustomerClassConfiguration()
        {
            this.HasRequired(m => m.Customer)
                .WithMany(m => m.Classes)
                .HasForeignKey(m => m.CustomerId)
                .WillCascadeOnDelete(false);

            this.HasRequired(m => m.Class)
                .WithMany(m => m.Customers)
                .HasForeignKey(m => m.ClassId)
                .WillCascadeOnDelete(false);
        }

        public static CustomerClassConfiguration Instance => new CustomerClassConfiguration();
    }
}
