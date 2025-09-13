namespace DapperDemo.Api.Infrastructure;

public class CustomExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<CustomExceptionHandler> logger, IHostEnvironment env) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An exception occurred.");
        var problem = exception.ToProblem(env.IsProduction());
        httpContext.Response.StatusCode = problem.Status ?? StatusCodes.Status500InternalServerError;
        
        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            Exception = exception,
            HttpContext = httpContext,
            ProblemDetails = problem
        });
    }
}