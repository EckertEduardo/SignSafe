namespace SignSafe.Presentation.ExceptionHandlers
{
    internal static class ExceptionHandlersSetup
    {
        internal static void AddExceptionHandlers(this IServiceCollection services)
        {
            services.AddExceptionHandler<ValidationExceptionHandler>();
            services.AddExceptionHandler<NotFoundExceptionHandler>();
            //Default
            services.AddExceptionHandler<GlobalExceptionHandler>();
        }
    }
}
