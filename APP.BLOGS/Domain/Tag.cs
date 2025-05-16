using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.BLOGS.Domain
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [InverseProperty("Tag")]
        public virtual ICollection<BlogTag> BlogTags { get; set; } = new List<BlogTag>();
    }
}
