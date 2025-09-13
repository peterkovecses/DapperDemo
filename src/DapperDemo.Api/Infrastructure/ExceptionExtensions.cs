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

        var errors = GetErrors(exception, statusCode);
        if (ProblemDetailsDefaults.Defaults.TryGetValue(statusCode, out var defaults))
        {
            return Problem(statusCode, defaults, detail, errors);
        }

        var title = ReasonPhrases.GetReasonPhrase(statusCode);
        if (string.IsNullOrEmpty(title))
        {
            title = "Error";
        }

        defaults = ("about:blank", title);
        
        return Problem(statusCode, defaults, detail, errors);
    }

    private static Dictionary<string, string[]>? GetErrors(Exception exception, int statusCode)

    {
        Dictionary<string, string[]>? errors = null;
        if (statusCode != StatusCodes.Status400BadRequest) return errors;
        var jsonEx = exception.GetInnerMost<JsonException>();
        if (jsonEx is null || string.IsNullOrEmpty(jsonEx.Path)) return errors;
        var propertyName = jsonEx.Path.TrimStart('$', '.');
        errors = new Dictionary<string, string[]>
        {
            [propertyName] = [FormatJsonException(jsonEx)]
        };

        return errors;
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

    private static ProblemDetails Problem(int statusCode, (string Type, string Title) defaults, string detail,
        Dictionary<string, string[]>? errors)
    {
        if (errors is not null && errors.Count > 0)
        {
            return new ValidationProblemDetails(errors)
            {
                Status = statusCode,
                Type = defaults.Type,
                Title = defaults.Title,
                Detail = detail
            };
        }

        return new ProblemDetails
        {
            Status = statusCode,
            Type = defaults.Type,
            Title = defaults.Title,
            Detail = detail
        };
    }
}