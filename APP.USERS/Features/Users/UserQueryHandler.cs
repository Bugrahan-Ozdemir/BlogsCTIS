using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP.Users.Features;
using APP.Users.Features.Skills;
using APP.USERS.Domain;
using APP.USERS.Features;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APP.Users.Features.Users
{
    public class UserQueryRequest : Request, IRequest<IQueryable<UserQueryResponse>>
    {
        
    }


    public class UserQueryResponse : QueryResponse
    {   
        public int RoleId { get; set; }
        public string RoleName { get; set; }
       
        public List<int> SkillIds { get; set; }
        public string SkillNames { get; set; }
        public List<SkillQueryResponse> Skills { get; set; }
        

        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsActive { get; set; }
        public string Active { get; set; }
         }
    public class UserQueryHandler : UsersDbHandler, IRequestHandler<UserQueryRequest, IQueryable<UserQueryResponse>>
    {
        public UserQueryHandler(BlogsUsersDb db) : base(db)
        {
        }

        public Task<IQueryable<UserQueryResponse>> Handle(UserQueryRequest request, CancellationToken cancellationToken)
        {
            IQueryable<UserQueryResponse> query = _db.Users.OrderBy(u => u.UserName).Select(t => new UserQueryResponse()
            {
                Id = t.Id,
                UserName = t.UserName,
                Name = t.Name,
                Surname = t.Surname,
                SkillIds = t.SkillIds,
                SkillNames = string.Join(", ", t.UserSkills.Select(us => us.Skill.Name)),
                Skills = t.UserSkills.Select(us => new SkillQueryResponse{Name = us.Skill.Name}).ToList(),
                RoleId = t.RoleId,
                RoleName = t.Role.Name,

            });

            return Task.FromResult(query);
        }
    }
}
