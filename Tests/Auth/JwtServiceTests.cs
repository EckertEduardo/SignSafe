using FluentAssertions;
using Microsoft.Extensions.Configuration;
using SignSafe.Application.Auth;
using System.IdentityModel.Tokens.Jwt;
using Tests.Fakers.Users;
using Tests.Helpers;

namespace Tests.Auth
{
    public class JwtServiceTests
    {
        private readonly IConfiguration _configuration;
        private readonly JwtService _jwtService;

        public JwtServiceTests()
        {
            _configuration = InitialSetup.SetupConfiguration();
            _jwtService = new JwtService(_configuration);
        }

        [Fact]
        public void GenerateToken_Should_ReturnToken_When_ValidUser()
        {
            //ARRANGE
            var user = new UserFaker().Generate();

            //ACT
            var result = _jwtService.GenerateToken(user);

            //ASSERT
            result.Should().BeOfType<string>();
            result.Should().Contain("Bearer");
        }

        [Fact]
        public void ConvertToken_Should_ReturnToken_When_ValidUser()
        {
            //ARRANGE
            var user = new UserFaker().Generate();
            var token = _jwtService.GenerateToken(user);

            //ACT
            var result = _jwtService.ConvertToken(token);

            //ASSERT
            result.Should().BeOfType<JwtSecurityToken>();
        }
    }
}