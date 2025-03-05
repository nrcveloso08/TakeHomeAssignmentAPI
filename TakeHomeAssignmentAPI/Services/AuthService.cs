using Microsoft.IdentityModel.Protocols.WSIdentity;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TakeHomeAssignmentAPI.Repositories;

namespace TakeHomeAssignmentAPI.Services
{
    public class AuthService
    {
        private readonly IConfiguration _config;
        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJwtToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                new[] { new Claim(ClaimTypes.Name, username) },
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpirationMinutes"])),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
