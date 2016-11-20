using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HiLaarIsch.Components;

namespace HiLaarIsch.Domain
{
    [Table("Classes")]
    public class ClassEntity
    {
        public ClassEntity()
        {
            this.Customers = new HashSet<CustomerClassEntity>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ClassType Type { get; set; }

        public Level Level { get; set; }

        public Day Day { get; set; }

        public ClassLength Length { get; set; }

        public TimeSpan Time { get; set; }

        public virtual ICollection<CustomerClassEntity> Customers { get; set; }
    }
}
