using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.BLOGS.Domain
{
    public class BlogTag
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("TagId")]

        [InverseProperty("BlogTags")]
        public virtual Tag Tag { get; set; } = null!;

        [ForeignKey("BlogId")]
        public int BlogId { get; set; }

        public int TagId { get; set; }

       
        [InverseProperty("BlogTags")]
        public virtual Blog Blog { get; set; } = null!;

        
    }
}
