using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.BLOGS.Domain
{
    public class Blog
    {

        [Key]
        public int Id { get; set; }

        [InverseProperty("Blog")]
        public virtual ICollection<BlogTag> BlogTags { get; set; } = new List<BlogTag>();

        [StringLength(200)]
        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        [Column(TypeName = "decimal(5, 2)")]
        public decimal? Rating { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime PublishDate { get; set; }
    }
}
