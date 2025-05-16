using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.USERS.Domain
{
    public class UserSkills
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("SkillId")]
        [InverseProperty("UserSkills")]
        public virtual Skill Skill { get; set; } = null!;

        [ForeignKey("UserId")]
        [InverseProperty("UserSkills")]
        public virtual User User { get; set; } = null!;
        public int UserId { get; set; }

        public int SkillId { get; set; }

       
      
    }
}
