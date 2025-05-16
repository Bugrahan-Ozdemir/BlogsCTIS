using APP.BLOGS.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.BLOGS.Features.Tags
{
    
        public class TagDeleteRequest : Request, IRequest<CommandResponse>
        {
        }

       
        public class TagDeleteHandler : BlogsDbHandler, IRequestHandler<TagDeleteRequest, CommandResponse>
        {
          
            public TagDeleteHandler(BlogsDb blogsDb) : base(blogsDb)
            {
            }

        
            public async Task<CommandResponse> Handle(TagDeleteRequest request, CancellationToken cancellationToken)
            {
                // Find the tag to delete.
                Tag tag = await _Blogsdb.Tags.SingleOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

                if (tag is null)
                    return Error("Tag not found!");

                
                _Blogsdb.Tags.Remove(tag);

                // Save the changes to the database by unit of work.
                await _Blogsdb.SaveChangesAsync(cancellationToken);

                return Success("Tag deleted successfully.");
            }
        }
    
}
