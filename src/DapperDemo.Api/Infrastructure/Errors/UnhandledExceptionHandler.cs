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
            ProblemDetails = GetProblem(exception, env)
        });
    }
    
    private static ProblemDetails GetProblem(Exception exception, IHostEnvironment env)
    {
        var (type, title) = Problem.GetDefinition(StatusCodes.Status500InternalServerError);
        var detail = env.IsProduction() ? "An error occurred while processing your request." : exception.Message;
        
        return Problem.Create(StatusCodes.Status500InternalServerError, type, title, detail);      
    }
}