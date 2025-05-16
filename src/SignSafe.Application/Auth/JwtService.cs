using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public UserTokenInfo? GetUserTokenInfo()
        {
            var token = _httpContextAccessor.HttpContext?.Request?.Headers["Authorization"].ToString()
                            ?? _httpContextAccessor.HttpContext?.Request?.Cookies["JwtToken"];

            if (string.IsNullOrEmpty(token))
                return null;

            var securityToken = ConvertToken(token!);

            var userIdIsValid = long.TryParse(securityToken.Claims.FirstOrDefault(x => x.Type == "userId")?.Value, out long userId);

            var email = securityToken.Claims
                .FirstOrDefault(x => x.Type.Equals(nameof(ClaimTypes.Email), StringComparison.OrdinalIgnoreCase))
                ?.Value;

            if (string.IsNullOrWhiteSpace(email) || !userIdIsValid)
                throw new Exception();

            var roles = securityToken.Claims
                .Where(x => x.Type.Equals(nameof(ClaimTypes.Role), StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Value)
                .ToArray();

            var userTokenInfo = new UserTokenInfo(userId, email, roles);
            return userTokenInfo;
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
            return tokenHandler.WriteToken(token);
        }

        public void RefreshToken(User user)
        {
            var token = GenerateToken(user);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Set to false if testing without HTTPS
                SameSite = SameSiteMode.None,
                Expires = ConvertToken(token).ValidTo,
                Path = "/"
            };

            _httpContextAccessor.HttpContext?.Response.Cookies.Append("jwt", token, cookieOptions);
        }
    }
}
