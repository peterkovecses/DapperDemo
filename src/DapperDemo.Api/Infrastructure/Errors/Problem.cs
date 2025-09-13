namespace DapperDemo.Api.Infrastructure.Errors;

public static class Problem
{
    public static (string Type, string Title) GetDefinition(int statusCode)
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
    
    public static ProblemDetails Create(int statusCode, string type, string title, string detail) =>
        new()
        {
            Status = statusCode,
            Type = type,
            Title = title,
            Detail = detail
        };
    
    public static ValidationProblemDetails CreateValidationProblem(int statusCode, string type, string title, string detail,
        Dictionary<string, string[]> errors) =>
        new(errors)
        {
            Status = statusCode,
            Type = type,
            Title = title,
            Detail = detail
        };
}