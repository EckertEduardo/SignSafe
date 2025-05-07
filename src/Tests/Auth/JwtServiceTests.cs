using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using NSubstitute;
using SignSafe.Application.Auth;
using SignSafe.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using Tests.Fakers.Users;
using Tests.Helpers;

namespace Tests.Auth
{
    public class JwtServiceTests
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtService _jwtService;

        public JwtServiceTests()
        {
            _configuration = InitialSetup.SetupConfiguration();
            _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            _jwtService = new JwtService(_configuration, _httpContextAccessor);
        }

        [Fact]
        public void GetUserTokenInfo_Should_Return_Null_When_TokenIsEmpty()
        {
            //ARRANGE
            //ACT
            var result = _jwtService.GetUserTokenInfo();

            //ASSERT
            result.Should().BeNull();
        }

        [Fact]
        public void GetUserTokenInfo_Should_ThrowException_When_UserIdIsNotValid_Or_EmailIsNotValid()
        {
            //ARRANGE
            _httpContextAccessor.HttpContext.Request.Headers["Authorization"].Returns(new StringValues("InvalidToken"));
            //ACT
            Action act = () => _jwtService.GetUserTokenInfo();

            //ASSERT
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void GetUserTokenInfo_Should_Return_UserTokenInfo_When_TokenIsValid()
        {
            //ARRANGE
            var user = new User("Test", "testc@test.com", "password", DateTime.Now);
            var token = _jwtService.GenerateToken(user);
            _httpContextAccessor.HttpContext.Request.Headers["Authorization"].Returns(new StringValues(token));
            //ACT
            var result = _jwtService.GetUserTokenInfo();

            //ASSERT
            result.Should().NotBeNull();
            result.Should().BeOfType<UserTokenInfo>();
        }

        [Fact]
        public void GenerateToken_Should_Return_Token_When_ValidUser()
        {
            //ARRANGE
            var user = new UserFaker().Generate();

            //ACT
            var result = _jwtService.GenerateToken(user);

            //ASSERT
            result.Should().BeOfType<string>();
            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ConvertToken_Should_Return_Token_When_ValidUser()
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