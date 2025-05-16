using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.USERS.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("RoleId")]
        [InverseProperty("Users")]
        public virtual Role Role { get; set; } = null!;

        [StringLength(100)]
        public string UserName { get; set; } = null!;

        [StringLength(100)]
        public string Password { get; set; } = null!;

        public bool IsActive { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(100)]
        public string Surname { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime RegistrationDate { get; set; }

        public int RoleId { get; set; }



        [InverseProperty("User")]
        public virtual ICollection<UserSkills> UserSkills { get; set; } = new List<UserSkills>();


        [NotMapped]
        public List<int> SkillIds
        {
            get => UserSkills?.Select(us => us.SkillId).ToList();
            set => UserSkills = value?.Select(v => new UserSkills() { SkillId = v }).ToList();
        }
    }
}
