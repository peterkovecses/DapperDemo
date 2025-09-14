namespace DapperDemo.Api.Infrastructure.Errors;

public static class BadHttpRequestExceptionExtensions
{
    public static ValidationProblemDetails ToProblem(this BadHttpRequestException exception)
    {
        var (type, title) = Problem.GetDefinition(StatusCodes.Status400BadRequest);
        var errors = exception.ExtractJsonErrors();
        const string detail = "The request could not be processed due to malformed input.";
        
        return Problem.CreateValidationProblem(StatusCodes.Status400BadRequest, type, title, detail, errors);        
    }
    
    private static Dictionary<string, string[]> ExtractJsonErrors(this BadHttpRequestException exception)
    {
        var jsonEx = exception.GetInnerMost<JsonException>();
        if (jsonEx is null || string.IsNullOrEmpty(jsonEx.Path)) return [];

        var propertyName = jsonEx.Path.TrimStart('$', '.');

        return new Dictionary<string, string[]>
        {
            [propertyName] = [FormatJsonException(jsonEx)]
        };
    }

    private static string FormatJsonException(JsonException ex)
    {
        var path = ex.Path is not null ? $"Path: {ex.Path}. " : "";
        return $"{ex.Message} {path}".Trim();
    }
}