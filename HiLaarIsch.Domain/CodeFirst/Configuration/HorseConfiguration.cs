using System.Data.Entity.ModelConfiguration;

namespace HiLaarIsch.Domain.CodeFirst.Configuration
{
    public class HorseConfiguration : EntityTypeConfiguration<HorseEntity>
    {
        public HorseConfiguration()
        {
            this.HasMany(m => m.Classes)
                .WithRequired(m => m.Horse)
                .Map(m => m.MapKey("FK_HorseId"))
                .WillCascadeOnDelete(false);
        }

        public static HorseConfiguration Instance => new HorseConfiguration();
    }
}
