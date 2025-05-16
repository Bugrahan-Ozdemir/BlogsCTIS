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
    // Represents the request to create a new tag
    public class TagCreateRequest : Request, IRequest<CommandResponse>
    {
        [Required, MaxLength(100), MinLength(3)]
        public string Name { get; set; }
    }

    public class TagCreateHandler : BlogsDbHandler, IRequestHandler<TagCreateRequest, CommandResponse>
    {
        public TagCreateHandler(BlogsDb blogsDb) : base(blogsDb)
        {
        }

        public async Task<CommandResponse> Handle(TagCreateRequest request, CancellationToken cancellationToken)
        {
           
            if (await _Blogsdb.Tags.AnyAsync(t => t.Name.ToUpper() == request.Name.ToUpper().Trim(), cancellationToken))
                return Error("Tag with the same name exists!");

            Tag tag = new Tag()
            {
                Name = request.Name.Trim()
            };

            // Add the tag to the database context
            // Way 1: does not insert relational data
            //_projectsDb.Entry(tag).State = EntityState.Added;
            // Way 2: inserts relational data
            //_Blogsdb.Add(tag);
            // Way 3: inserts relational data
            _Blogsdb.Tags.Add(tag);

            // Save changes asynchronously (unit of work pattern)
            await _Blogsdb.SaveChangesAsync(cancellationToken);

            // Return a successful command response with the ID of the newly created tag
            // Note: This should ideally use the Success method from the base class instead of manually constructing the response
            return Success("Tag created successfully.", tag.Id);
        }
    }
}
