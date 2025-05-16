using APP.BLOGS.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.BLOGS.Features.Blog
{
    public class BlogDeleteRequest : Request, IRequest<CommandResponse>
    {
        [Required]
        public int Id { get; set; }
    }
    public class BlogDeleteHandler : BlogsDbHandler, IRequestHandler<BlogDeleteRequest, CommandResponse>
    {
        public BlogDeleteHandler(BlogsDb projectsDb) : base(projectsDb)
        {
        }
        public async Task<CommandResponse> Handle(BlogDeleteRequest request, CancellationToken cancellationToken)
        {
            var blogEntity = await _Blogsdb.Blogs.Include(b => b.BlogTags).SingleOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (blogEntity == null)
                return Error("Blog not found!");

            _Blogsdb.BlogTags.RemoveRange(blogEntity.BlogTags);

            _Blogsdb.Blogs.Remove(blogEntity);
            await _Blogsdb.SaveChangesAsync(cancellationToken);

            return Success("Blog deleted successfully.", blogEntity.Id);
        }
    }
}
