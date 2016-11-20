using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiLaarIsch.Domain
{
    [Table("CustomerClasses")]
    public class CustomerClassEntity
    {
        public CustomerClassEntity()
        {
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("FK_CustomerId", Order = 0)]
        public int CustomerId { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("FK_ClassId", Order = 1)]
        public int ClassId { get; set; }

        public virtual CustomerEntity Customer { get; set; }
        public virtual ClassEntity Class { get; set; }
        public virtual HorseEntity Horse { get; set; }
    }
}
