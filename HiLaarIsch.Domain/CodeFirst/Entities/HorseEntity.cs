using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiLaarIsch.Domain
{
    [Table("Horses")]
    public class HorseEntity : IEntity
    {
        public HorseEntity()
        {
            this.Classes = new HashSet<CustomerClassAssociation>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<CustomerClassAssociation> Classes { get; set; }
    }
}
