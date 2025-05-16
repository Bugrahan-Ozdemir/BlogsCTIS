using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.BLOGS.Domain
{
    public class BlogsDb : DbContext
    {

        public BlogsDb()
        {
        }

        public BlogsDb(DbContextOptions<BlogsDb> options) : base(options)
        {
        }

        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<BlogTag> BlogTags { get; set; }



        public virtual DbSet<Tag> Tags { get; set; }
    }
    }
