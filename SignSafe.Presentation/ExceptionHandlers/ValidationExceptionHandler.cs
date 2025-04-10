using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace SignSafe.Presentation.ExceptionHandlers
{
    internal sealed class ValidationExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ValidationExceptionHandler> _logger;
        private readonly IHostEnvironment _env;

        public ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger, IHostEnvironment env)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is ValidationException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                _logger.LogInformation(
                    exception, "One or more validation errors occurred: {Message}", exception.Message);

                var problemDetails = CreateProblemDetails(httpContext, exception);

                await httpContext.Response.WriteAsJsonAsync(
                    value: problemDetails,
                    options: null,
                    contentType: "application/json",
                    cancellationToken: cancellationToken
                    );

                return true;
            }
            return false;
        }

        private HttpValidationProblemDetails CreateProblemDetails(in HttpContext httpContext, in Exception exception)
        {
            var validationException = (ValidationException)exception;
            var errors = validationException.Errors
                      .GroupBy(f => f.PropertyName)
                      .ToDictionary(
                          group => group.Key,
                          group => group.Select(f => f.ErrorMessage).ToArray()
                      );

            var problemDetails = new HttpValidationProblemDetails(errors)
            {
                Title = "One or more validation errors occurred.",
                Status = httpContext.Response.StatusCode,
                Type = exception.GetType().Name,
            };

            if ((_env.IsDevelopment() || _env.IsStaging()) && false)
            {
                problemDetails.Detail = exception.Message;
            }

            return problemDetails;
        }
    }
}
