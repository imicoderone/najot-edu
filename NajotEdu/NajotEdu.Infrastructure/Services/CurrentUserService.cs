using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NajotEdu.Application.Abstractions;

namespace NajotEdu.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public int UserId { get; set; }

        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            var userClaims = contextAccessor.HttpContext!.User.Claims;

            var idClaim = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name);

            if (idClaim != null && int.TryParse(idClaim.Value, out int value))
            {
                UserId = value;
            }
        }
    }
}
