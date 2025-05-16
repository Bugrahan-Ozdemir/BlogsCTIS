using APP.USERS.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APP.USERS.Features.Users
{
    public class TokenRequest : Request, IRequest<TokenResponse>
    {
        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [JsonIgnore]
        public override int Id { get => base.Id; set => base.Id = value; }
    }

    public class TokenResponse : CommandResponse
    {
        public string Token { get; set; }

        public TokenResponse(bool isSuccessful, string message = "", int id = 0) : base(isSuccessful, message, id)
        {
        }
    }

    public class TokenHandler : UsersDbHandler, IRequestHandler<TokenRequest, TokenResponse>
    {
        public TokenHandler(BlogsUsersDb db) : base(db)
        {
        }

        public async Task<TokenResponse> Handle(TokenRequest request, CancellationToken cancellationToken)
        {
            var user = await _db.Users.Include(u => u.Role).SingleOrDefaultAsync(u =>
                u.UserName == request.UserName && u.Password == request.Password && u.IsActive);
            if (user is null)
                return new TokenResponse(false, "Active user with the user name and password not found");
            var claims = GetClaims(user);
            var expiration = DateTime.Now.AddMinutes(AppSettings.ExpirationInMinutes);
            var token = CreateAccessToken(claims, expiration);
            return new TokenResponse(true, "Token created successfully.", user.Id)
            {
                Token = JwtBearerDefaults.AuthenticationScheme + " " + token
            };
        }
    }
}
