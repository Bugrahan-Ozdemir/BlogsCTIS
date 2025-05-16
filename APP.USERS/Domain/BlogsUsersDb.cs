using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.USERS.Domain
{
    public class BlogsUsersDb : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkills> UserSkills { get; set; }

        public BlogsUsersDb(DbContextOptions options) : base(options)
        {
        }
    }
}
