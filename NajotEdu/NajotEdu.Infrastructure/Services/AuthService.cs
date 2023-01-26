using Microsoft.EntityFrameworkCore;
using NajotEdu.Infrastructure.Abstractions;
using NajotEdu.Infrastructure.Persistence;
using NajotEdu.Infrastructure.Utils;

namespace NajotEdu.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ITokenService _tokenService;

        public AuthService(ApplicationDbContext dbContext, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (user.PasswordHash != HashGenerator.Generate(password))
            {
                throw new Exception("Password is wrong");
            }

            return _tokenService.GenerateAccessToken(user);
        }
    }
}
