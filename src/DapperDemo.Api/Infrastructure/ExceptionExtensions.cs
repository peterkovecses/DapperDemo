namespace DapperDemo.Api.Infrastructure;

public static class ExceptionExtensions
{
    public static ProblemDetails ToProblem(this Exception exception, IHostEnvironment env)
    {
        var (statusCode, detail) = exception switch
        {
            BadHttpRequestException badRequestException => (StatusCodes.Status400BadRequest,
                "The request could not be processed due to malformed input."),
            _ => (StatusCodes.Status500InternalServerError,
                env.IsProduction() ? "An error occurred while processing your request." : exception.Message)
        };
        
        var (type, title) = GetProblemDefinition(statusCode);
        if (statusCode != StatusCodes.Status400BadRequest) return Problem(statusCode, type, title, detail);
        var errors = GetErrors(exception);

        return ValidationProblem(statusCode, type, title, detail, errors);

    }

    private static (string Type, string Title) GetProblemDefinition(int statusCode)
    {
        if (ProblemDetailsDefaults.Defaults.TryGetValue(statusCode, out var defaults)) return defaults;
        var title = ReasonPhrases.GetReasonPhrase(statusCode);
        if (string.IsNullOrEmpty(title))
        {
            title = "Error";
        }

        defaults = ("about:blank", title);

        return defaults;
    }

    private static Dictionary<string, string[]> GetErrors(Exception exception)

    {
        var jsonEx = exception.GetInnerMost<JsonException>();
        if (jsonEx is null || string.IsNullOrEmpty(jsonEx.Path)) return [];
        var propertyName = jsonEx.Path.TrimStart('$', '.');
        
        return new Dictionary<string, string[]>
        {
            [propertyName] = [FormatJsonException(jsonEx)]
        };
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

    private static ProblemDetails Problem(int statusCode, string type, string title, string detail) =>
        new()
        {
            Status = statusCode,
            Type = type,
            Title = title,
            Detail = detail
        };

    private static ValidationProblemDetails ValidationProblem(int statusCode, string type, string title, string detail,
        Dictionary<string, string[]> errors) =>
        new(errors)
        {
            Status = statusCode,
            Type = type,
            Title = title,
            Detail = detail
        };
}