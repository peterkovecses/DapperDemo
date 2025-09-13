namespace DapperDemo.Api.Infrastructure.Errors;

public static class BadHttpRequestExceptionExtensions
{
    public static Dictionary<string, string[]> ExtractJsonErrors(this BadHttpRequestException exception)
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