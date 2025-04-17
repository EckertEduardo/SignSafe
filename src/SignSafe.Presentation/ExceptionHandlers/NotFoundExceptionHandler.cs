using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SignSafe.Domain.Exceptions;
using System.Net;

namespace SignSafe.Presentation.ExceptionHandlers
{
    internal sealed class NotFoundExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<NotFoundExceptionHandler> _logger;

        public NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is NotFoundException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

                _logger.LogInformation(
                    exception, "{NotFoundMessage}", exception.Message);

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

        private ProblemDetails CreateProblemDetails(in HttpContext httpContext, in Exception exception)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Some expected data was not found",
                Status = httpContext.Response.StatusCode,
                Type = exception.GetType().Name,
                Detail = exception.Message
            };

            return problemDetails;
        }
    }
}
