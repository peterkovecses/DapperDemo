namespace DapperDemo.Api.Infrastructure.Errors;

public class BadRequestExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<BadRequestExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not BadHttpRequestException badRequestException) return false;
        logger.LogError(exception, "An exception occurred.");
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        
        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            Exception = exception,
            HttpContext = httpContext,
            ProblemDetails = badRequestException.ToProblem()
        });
    }
}