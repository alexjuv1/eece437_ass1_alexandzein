using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniversityApp.Models;

namespace UniversityApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _um;
        private readonly SignInManager<ApplicationUser> _sm;
        private readonly IConfiguration _cfg;

        public AuthController(UserManager<ApplicationUser> um, SignInManager<ApplicationUser> sm, IConfiguration cfg)
        {
            _um = um; _sm = sm; _cfg = cfg;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _um.FindByNameAsync(dto.Username);
            if (user == null) return Unauthorized();
            var result = await _sm.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded) return Unauthorized();

            var token = GenerateToken(user);
            return Ok(new { token });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var user = await _um.GetUserAsync(User);
            return Ok(new
            {
                user.UserName,
                user.Email,
                user.FirstName,
                user.LastName
            });
        }

        [Authorize]
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePwdDto dto)
        {
            var user = await _um.GetUserAsync(User);
            var result = await _um.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return NoContent();
        }

        // Logout is client-side: just discard token

        private string GenerateToken(ApplicationUser user)
        {
            // 1️⃣ Build claims list, including NameIdentifier so GetUserAsync can find them
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),      // <- important!
                new Claim(ClaimTypes.Name,           user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // 2️⃣ Create signing key & creds
            var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 3️⃣ Build the token
            var token = new JwtSecurityToken(
                issuer:           _cfg["Jwt:Issuer"],
                audience:         _cfg["Jwt:Issuer"],   
                claims:           claims,
                expires:          DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            // 4️⃣ Return the serialized token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
