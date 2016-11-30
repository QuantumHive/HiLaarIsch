using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HiLaarIsch.Components;

namespace HiLaarIsch.Domain
{
    [Table("Users")]
    public partial class UserEntity : IEntity
    {
        public UserEntity()
        {
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required, StringLength(100)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        [StringLength(100)]
        public string PasswordHash { get; set; }

        public Role Role { get; set; }

        public virtual CustomerEntity Customer { get; set; }
    }
}
