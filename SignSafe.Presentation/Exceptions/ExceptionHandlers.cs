using SignSafe.Presentation.Exceptions.Handlers;

namespace SignSafe.Presentation.Exceptions
{
    internal static class ExceptionHandlers
    {
        internal static void AddExceptionHandlers(this IServiceCollection services)
        {
            services.AddExceptionHandler<ValidationExceptionHandler>();
            //Default
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
        }
    }
}
