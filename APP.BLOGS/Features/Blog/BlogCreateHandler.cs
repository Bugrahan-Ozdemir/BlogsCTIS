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
   
    public class BlogCreateRequest : Request, IRequest<CommandResponse>
    {
       
        [Required, StringLength(200, MinimumLength = 5)]
        public string Title { get; set; }

       
        [Required, StringLength(4000)]
        public string Content { get; set; }

       
        public decimal? Rating { get; set; }

        
        [Required]
        public DateTime PublishDate { get; set; }

        
        public List<int> TagIds { get; set; }
    }

    
    public class BlogCreateHandler : BlogsDbHandler, IRequestHandler<BlogCreateRequest, CommandResponse>
    {
        public BlogCreateHandler(BlogsDb projectsDb) : base(projectsDb)
        {
        }

        public async Task<CommandResponse> Handle(BlogCreateRequest request, CancellationToken cancellationToken)
        {
            var exists = await _Blogsdb.Blogs.AnyAsync(
                b => b.Title.ToUpper() == request.Title.Trim().ToUpper(),
                cancellationToken
            );
            if (exists)
                return Error("A blog with the same title already exists!");

            var blogEntity = new APP.BLOGS.Domain.Blog
            {
                Title = request.Title.Trim(),
                Content = request.Content.Trim(),
                Rating = request.Rating,
                PublishDate = request.PublishDate,
            };

            _Blogsdb.Blogs.Add(blogEntity);
            await _Blogsdb.SaveChangesAsync(cancellationToken);

            if (request.TagIds != null && request.TagIds.Any())
            {
                var blogTags = request.TagIds.Select(tagId => new BlogTag
                {
                    BlogId = blogEntity.Id,
                    TagId = tagId
                });
                _Blogsdb.BlogTags.AddRange(blogTags);
                await _Blogsdb.SaveChangesAsync(cancellationToken);
            }

            return Success("Blog created successfully.", blogEntity.Id);
        }
    }
}