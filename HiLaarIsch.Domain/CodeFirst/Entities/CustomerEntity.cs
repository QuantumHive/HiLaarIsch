using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HiLaarIsch.Components;

namespace HiLaarIsch.Domain
{
    [Table("Customers")]
    public partial class CustomerEntity : IEntity
    {
        public CustomerEntity()
        {
            this.Classes = new HashSet<CustomerClassAssociation>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Surname { get; set; }

        [Required]
        [StringLength(100)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(100)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string EmergencyNumber { get; set; }

        public Level Level { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual ICollection<CustomerClassAssociation> Classes { get; set; }
    }
}