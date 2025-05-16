using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignSafe.Application.Auth;
using SignSafe.Application.Users.Commands.Delete;
using SignSafe.Application.Users.Commands.Disable;
using SignSafe.Application.Users.Commands.Enable;
using SignSafe.Application.Users.Commands.Insert;
using SignSafe.Application.Users.Commands.ResetPassword;
using SignSafe.Application.Users.Commands.SendOtpEmail;
using SignSafe.Application.Users.Commands.SendOtpEmailResetPassword;
using SignSafe.Application.Users.Commands.Update;
using SignSafe.Application.Users.Commands.UpdateRole;
using SignSafe.Application.Users.Commands.VerifyAccount;
using SignSafe.Application.Users.Queries.Get;
using SignSafe.Application.Users.Queries.GetAll;
using SignSafe.Application.Users.Queries.GetLogged;
using SignSafe.Application.Users.Queries.IsLogged;
using SignSafe.Application.Users.Queries.IsOtpValid;
using SignSafe.Application.Users.Queries.Login;
using SignSafe.Presentation.Attributes;

namespace SignSafe.Presentation.Controllers
{
    [Route("api/users")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("is-logged")]
        public async Task<IActionResult> IsLogged()
        {
            var query = new UserIsLoggedQuery();

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("is-otp-valid")]
        public async Task<IActionResult> IsOtpValid([FromHeader] IsOtpValidQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-logged-user")]
        public async Task<IActionResult> GetLoggedUser()
        {
            var query = new GetLoggedUserQuery();

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-by-filter:paginated")]
        public async Task<IActionResult> GetByFilter([FromQuery] GetUsersByFilterQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery] GetUserQuery query)
        {
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound($"({nameof(query.UserId)}: {query.UserId}) was not found");
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery query)
        {
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return Unauthorized("Incorrect Email or Password! Please, try again.");
            }
            if (!result.Enabled)
            {
                return Unauthorized("User disabled! Please contact an administrator.");
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Set to false if testing without HTTPS
                SameSite = SameSiteMode.None,
                Expires = result.ExpiresIn,
                Path = "/"
            };

            Response.Cookies.Append("jwt", result.JwtToken, cookieOptions);
            return Ok(result);
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Set to false if testing without HTTPS
                SameSite = SameSiteMode.None,
                Path = "/"
            };

            Response.Cookies.Delete("jwt", cookieOptions);
            return Ok();
        }

        [HttpPost]
        [Route("send-otp-email")]
        public async Task<IActionResult> SendOtpEmail()
        {
            await _mediator.Send(new SendOtpEmailCommand());
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("send-otp-email-reset-password")]
        public async Task<IActionResult> SendOtpEmailResetPassword([FromBody] SendOtpEmailResetPasswordCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost]
        [Route("verify-account")]
        public async Task<IActionResult> VerifyAccount([FromBody] VerifyAccountCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("insert")]
        public async Task<IActionResult> Insert([FromBody] InsertUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Created("null", value: result);
        }

        [HttpPut]
        [Roles(RolesScheme.Admin)]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPatch]
        //[Roles(RolesScheme.Admin)]
        [Route("update-roles")]
        public async Task<IActionResult> UpdateRoles([FromQuery] UpdateRolesCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPatch]
        //[Roles(RolesScheme.Admin)]
        [Route("enable/{id}")]
        public async Task<IActionResult> Enable(long id)
        {
            var command = new EnableUserCommand { UserId = id };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPatch]
        //[Roles(RolesScheme.Admin)]
        [Route("disable/{id}")]
        public async Task<IActionResult> Disable(long id)
        {
            var command = new DisableUserCommand { UserId = id };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        //[Roles(RolesScheme.Admin)]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var command = new DeleteUserCommand { UserId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
