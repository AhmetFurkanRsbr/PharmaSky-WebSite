using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pharmacy.Persistence.Context;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly PharmaSkyDbContext _context;

        public AuthController(IConfiguration config, PharmaSkyDbContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("token")]
        public IActionResult GetToken([FromBody] LoginRequest request)
        {
            // 1. Client doğrula
            var clients = _config.GetSection("OAuth2:Clients").Get<List<OAuthClient>>();
            var client = clients.FirstOrDefault(c => c.ClientId == request.ClientId && c.ClientSecret == request.ClientSecret);
            if (client == null)
                return Unauthorized("Geçersiz tarayıcı bilgileri");

            // 2. Kullanıcı doğrula 
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);
            if (user == null) return Unauthorized(new { message = "Kullanıcı yok" });

            if (user.Password != request.Password) return Unauthorized(new { message = "Şifre hatalı" });

    

            // 3. Token üret
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, request.Username),
            new Claim("client_id", request.ClientId),
            new Claim(ClaimTypes.Role, user.Role ?? "")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);



            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:AccessTokenExpirationMinutes"]!)),
                signingCredentials: creds
            );

            return Ok(new { access_token = new JwtSecurityTokenHandler().WriteToken(token), user.Role });
        }
    }

}
