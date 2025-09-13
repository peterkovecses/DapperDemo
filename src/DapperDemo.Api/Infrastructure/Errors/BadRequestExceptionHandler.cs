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
            ProblemDetails = GetProblem(badRequestException)
        });
    }

    private static ValidationProblemDetails GetProblem(BadHttpRequestException exception)
    {
        var (type, title) = Problem.GetDefinition(StatusCodes.Status400BadRequest);
        var errors = exception.ExtractJsonErrors();
        const string detail = "The request could not be processed due to malformed input.";
        
        return Problem.CreateValidationProblem(StatusCodes.Status400BadRequest, type, title, detail, errors);        
    }
}