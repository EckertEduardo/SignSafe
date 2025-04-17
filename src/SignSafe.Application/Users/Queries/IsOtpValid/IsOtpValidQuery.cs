using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SignSafe.Application.Users.Queries.IsOtpValid
{
    public sealed class IsOtpValidQuery : IRequest<bool>
    {
        [FromHeader(Name = "email")]
        public string? Email { get; set; }

        [Required]
        [FromHeader(Name = "otpVerificationCode")]
        public string OtpVerificationCode { get; set; } = string.Empty;
    }
}
