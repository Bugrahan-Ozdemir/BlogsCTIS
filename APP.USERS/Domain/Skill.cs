using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.USERS.Domain
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [InverseProperty("Skill")]
        public virtual ICollection<UserSkills> UserSkills { get; set; } = new List<UserSkills>();
    }
}
