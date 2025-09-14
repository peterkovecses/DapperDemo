namespace DapperDemo.Api.Infrastructure.Errors;

public class UnhandledExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<UnhandledExceptionHandler> logger, IHostEnvironment env) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An exception occurred.");
        
        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            Exception = exception,
            HttpContext = httpContext,
            ProblemDetails = exception.ToProblem(env.IsProduction())
        });
    }
}