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
    public class BlogUpdateRequest : Request, IRequest<CommandResponse>
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(200, MinimumLength = 5)]
        public string Title { get; set; }

        [Required, StringLength(4000)]
        public string Content { get; set; }

        public decimal? Rating { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        
        public List<int> TagIds { get; set; }
    }
    public class BlogUpdateHandler : BlogsDbHandler, IRequestHandler<BlogUpdateRequest, CommandResponse>
    {
        public BlogUpdateHandler(BlogsDb projectsDb) : base(projectsDb)
        {
        }

        public async Task<CommandResponse> Handle(BlogUpdateRequest request, CancellationToken cancellationToken)
        {
            bool titleConflict = await _Blogsdb.Blogs.AnyAsync(b => b.Id != request.Id && b.Title.ToUpper() == request.Title.Trim().ToUpper(),cancellationToken);
            if (titleConflict)
                return Error("Another blog with the same title already exists!");

                var blogEntity = await _Blogsdb.Blogs.Include(b => b.BlogTags).SingleOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (blogEntity == null)
                return Error("Blog not found!");

            _Blogsdb.BlogTags.RemoveRange(blogEntity.BlogTags);

            blogEntity.Title = request.Title.Trim();
            blogEntity.Content = request.Content.Trim();
            blogEntity.Rating = request.Rating;
            blogEntity.PublishDate = request.PublishDate;

            if (request.TagIds != null && request.TagIds.Any())
            {
                var newTags = request.TagIds.Select(tagId => new BlogTag
                {
                    BlogId = blogEntity.Id,
                    TagId = tagId
                });
                _Blogsdb.BlogTags.AddRange(newTags);
            }

            _Blogsdb.Blogs.Update(blogEntity);
            await _Blogsdb.SaveChangesAsync(cancellationToken);
            return Success("Blog updated successfully.", blogEntity.Id);
        }
    }
}
