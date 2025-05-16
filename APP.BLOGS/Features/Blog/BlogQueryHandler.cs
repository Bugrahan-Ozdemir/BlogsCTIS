using APP.BLOGS.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.BLOGS.Features.Blog
{
    public class BlogQueryHandler
    {
       
        public class BlogQueryRequest : Request, IRequest<IQueryable<BlogQueryResponse>>
        {
         
        }

      
        public class BlogQueryResponse : QueryResponse
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public decimal? Rating { get; set; }
            public DateTime PublishDate { get; set; }
            
            public List<int> TagIds { get; set; }
        }

        
        public class BlogsQueryHandler : BlogsDbHandler, IRequestHandler<BlogQueryRequest, IQueryable<BlogQueryResponse>>
        {
            public BlogsQueryHandler(BlogsDb projectsDb) : base(projectsDb)
            {
            }

            public Task<IQueryable<BlogQueryResponse>> Handle(BlogQueryRequest request, CancellationToken cancellationToken)
            {
                
                var query = _Blogsdb.Blogs.Include(b => b.BlogTags).ThenInclude(bt => bt.Tag).Select(b => new BlogQueryResponse
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Content = b.Content,
                        Rating = b.Rating,
                        PublishDate = b.PublishDate,
                        TagIds = b.BlogTags.Select(bt => bt.TagId).ToList()
                    }).OrderBy(b => b.Title);

                return Task.FromResult<IQueryable<BlogQueryResponse>>(query);

            }
        }
    }
}
