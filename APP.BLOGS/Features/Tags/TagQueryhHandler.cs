using APP.BLOGS.Domain;
using CORE.APP.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.BLOGS.Features.Tags
{
   
    public class TagQueryRequest : Request, IRequest<IQueryable<TagQueryResponse>>
    {
    }

    public class TagQueryResponse : QueryResponse
    {
       
        public string Name { get; set; }
    }

    
    public class TagQueryHandler : BlogsDbHandler, IRequestHandler<TagQueryRequest, IQueryable<TagQueryResponse>>
    {
       
        public TagQueryHandler(BlogsDb blogsDb) : base(blogsDb)
        {
        }
        public Task<IQueryable<TagQueryResponse>> Handle(TagQueryRequest request, CancellationToken cancellationToken)
        {
            // SQL: select Id, Name from Tags order by Name
            return Task.FromResult(_Blogsdb.Tags.OrderBy(tag => tag.Name).Select(tag => new TagQueryResponse()
            {
                Id = tag.Id,
                Name = tag.Name
            }));
        }
    }
}
