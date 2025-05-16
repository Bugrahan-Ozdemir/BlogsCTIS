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

namespace APP.BLOGS.Features.Tags
{
    public class TagUpdateRequest : Request, IRequest<CommandResponse>
    {
      
        [Required, MaxLength(100), MinLength(3)]
        public string Name { get; set; }
    }

  
    public class TagUpdateHandler : BlogsDbHandler, IRequestHandler<TagUpdateRequest, CommandResponse>
    {
     
        public TagUpdateHandler(BlogsDb blogsDb) : base(blogsDb)
        {
        }

    
        public async Task<CommandResponse> Handle(TagUpdateRequest request, CancellationToken cancellationToken)
        {
            // Check if a tag with the same name already exists other than the record to be updated by using the Id condition.
            if (await _Blogsdb.Tags.AnyAsync(t => t.Id != request.Id && t.Name.ToUpper() == request.Name.ToUpper().Trim(), cancellationToken))
                return Error("Tag with the same name exists!");

         
            Tag tag = await _Blogsdb.Tags.SingleOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (tag is null)
                return Error("Tag not found!");

            // Update the tag name.
            tag.Name = request.Name.Trim();

           
            _Blogsdb.Tags.Update(tag);

            await _Blogsdb.SaveChangesAsync(cancellationToken);

            return Success("Tag updated successfully.", tag.Id);
        }
    }
}
