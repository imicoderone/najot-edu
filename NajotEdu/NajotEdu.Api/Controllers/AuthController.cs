using Microsoft.AspNetCore.Mvc;
using NajotEdu.Api.Models;
using NajotEdu.Infrastructure.Abstractions;

namespace NajotEdu.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            var token = await _authService.LoginAsync(request.UserName, request.Password);

            return Ok(token);
        }

    }
}
