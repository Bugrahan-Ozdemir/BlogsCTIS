using APP.BLOGS.Domain;
using CORE.APP.Features;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.BLOGS.Features
{
    public abstract class BlogsDbHandler : Handler
    {
        protected readonly BlogsDb _Blogsdb;

        protected BlogsDbHandler(BlogsDb Blogsdb) : base(new CultureInfo("en-US")) // tr-TR: Turkish
        {
            _Blogsdb = Blogsdb;
        }
    }
}
