namespace DapperDemo.Api.Infrastructure;

public static class ExceptionExtensions
{
    public static ProblemDetails ToProblem(this Exception exception, IHostEnvironment env)
    {
        var (statusCode, detail) = exception switch
        {
            BadHttpRequestException badRequestException => (StatusCodes.Status400BadRequest, badRequestException.GetDetail()),
            _ => (StatusCodes.Status500InternalServerError, env.IsProduction() ? "An error occurred while processing your request." : exception.Message)
        };

        if (ProblemDetailsDefaults.Defaults.TryGetValue(statusCode, out var defaults))
        {
            return Problem(statusCode, defaults, detail);
        }
        
        var title = ReasonPhrases.GetReasonPhrase(statusCode);
        if (string.IsNullOrEmpty(title))
        {
            title = "Error";
        }
            
        defaults = ("about:blank", title);

        return Problem(statusCode, defaults, detail);
    }

    private static string GetDetail(this BadHttpRequestException exception, bool includeDeveloperDetails = false)
    {
        var jsonException = exception.GetInnerMost<JsonException>();

        return jsonException is not null
            ? FormatJsonException(jsonException)
            : "The request could not be processed due to malformed input.";
    }
    
    private static TException? GetInnerMost<TException>(this Exception exception)
        where TException : Exception
    {
        while (exception.InnerException is not null)
        {
            exception = exception.InnerException;
            if (exception is TException target) return target;
        }

        return null;
    }

    private static string FormatJsonException(JsonException ex)
    {
        var path = ex.Path is not null ? $"Path: {ex.Path}. " : "";
        
        return $"{ex.Message} {path}".Trim();
    }
    
    private static ProblemDetails Problem(int statusCode, (string Type, string Title) defaults, string detail) =>
        new()
        {
            Status = statusCode,
            Type = defaults.Type,
            Title = defaults.Title,
            Detail = detail
        };
}