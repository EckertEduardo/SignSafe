using MediatR;
using Microsoft.Extensions.Logging;

namespace SignSafe.Application.Behaviors
{
    public sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {

        private readonly ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> _logger;

        public RequestLoggingPipelineBehavior(ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogInformation("Initiating the request {RequestName}", requestName);

            TResponse result = await next();

            _logger.LogInformation("Finishing the request {RequestName}", requestName);
            return result;
        }
    }
}
