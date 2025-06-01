namespace DapperDemo.Api.Infrastructure;

public class CustomExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An exception occurred.");
        var error = MapException(exception);
        var problemDetails = new ProblemDetails
        {
            Type = exception.GetType().Name,
            Title = error.Title,
            Status = error.StatusCode,
            Detail = exception.Message
        };

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            Exception = exception,
            HttpContext = httpContext,
            ProblemDetails = problemDetails
        });
    }
    
    private static (int StatusCode, string Title) MapException(Exception exception)
        => exception switch
        {
            _ => (StatusCodes.Status500InternalServerError, "https://www.rfc-editor.org/rfc/rfc9110.html#section-15.6.1")
        };
}