using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SignSafe.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SignSafe.Application.Auth
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public JwtSecurityToken ConvertToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            token = token.Replace("Bearer ", "");

            var securityToken = handler.ReadJwtToken(token);
            return securityToken;
        }
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWT:Secret").Value!);
            var claims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var roleList = user.Roles.Split(',').ToList();
            roleList.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = _configuration.GetSection("JWT:Audience").Value,
                Issuer = _configuration.GetSection("JWT:Issuer").Value,
                Expires = DateTime.UtcNow.AddHours(Convert.ToInt32(_configuration.GetSection("JWT:ExpiresInHours").Value)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return string.Concat("Bearer ", tokenHandler.WriteToken(token));
        }
    }
}
