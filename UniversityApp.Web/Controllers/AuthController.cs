using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using UniversityApp.Infrastructure.Identity;    // ApplicationUser
using UniversityApp.Application.Interfaces;    // ITokenService
using UniversityApp.Application.DTOs;           // e.g. LoginDto, RegisterDto
using System.Threading.Tasks;

namespace UniversityApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _users;
        private readonly ITokenService                _tokens;

        public AuthController(UserManager<ApplicationUser> users, ITokenService tokens)
        {
            _users  = users;
            _tokens = tokens;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var user = new ApplicationUser { UserName = dto.Email, Email = dto.Email };
            var result = await _users.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // assign default role
            await _users.AddToRoleAsync(user, dto.Role);

            var token = await _tokens.CreateTokenAsync(user.Id);
            return Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _users.FindByEmailAsync(dto.Email);
            if (user == null || !await _users.CheckPasswordAsync(user, dto.Password))
                return Unauthorized("Invalid credentials");

            var token = await _tokens.CreateTokenAsync(user.Id);
            return Ok(new { token });
        }
    }
}
