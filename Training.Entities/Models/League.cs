using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Training.Entities.Models
{
    public class League
    {
        [Key]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}