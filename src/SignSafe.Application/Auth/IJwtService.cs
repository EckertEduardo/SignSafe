using SignSafe.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace SignSafe.Application.Auth
{
    public interface IJwtService
    {
        UserTokenInfo? GetUserTokenInfo();
        JwtSecurityToken ConvertToken(string token);
        string GenerateToken(User user);
    }
}
