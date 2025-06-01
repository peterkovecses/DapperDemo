using System.Text.Json;

namespace DapperDemo.Api.Infrastructure;

public static class ExceptionExtensions
{
    public static string GetDetail(this BadHttpRequestException exception, bool includeDeveloperDetails = false)
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
}