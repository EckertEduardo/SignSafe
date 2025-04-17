using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;

namespace SignSafe.Presentation.ExceptionHandlers
{
    internal sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment env)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(
                exception, "Exception occurred: {Message}", exception.Message);

            var problemDetails = CreateProblemDetails(httpContext, exception);

            await httpContext.Response.WriteAsJsonAsync(
                value: problemDetails,
                options: null,
                contentType: "application/problem+json",
                cancellationToken: cancellationToken
                );

            return true;
        }

        private ProblemDetails CreateProblemDetails(in HttpContext httpContext, in Exception exception)
        {
            var statusCode = httpContext.Response.StatusCode;
            var reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode);
            if (string.IsNullOrEmpty(reasonPhrase))
            {
                reasonPhrase = "An unexpected exception ocurred while executing the request";
            }

            var problemDetails = new ProblemDetails
            {
                Title = reasonPhrase,
                Status = statusCode,
                Type = exception.GetType().Name,
            };

            if ((_env.IsDevelopment() || _env.IsStaging()) && false)
            {
                problemDetails.Detail = exception.Message;
                problemDetails.Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}";
                problemDetails.Extensions["traceId"] = Activity.Current?.Id;
                problemDetails.Extensions["requestId"] = httpContext.TraceIdentifier;
                problemDetails.Extensions["data"] = exception.Data;
            }

            return problemDetails;
        }
    }
}
