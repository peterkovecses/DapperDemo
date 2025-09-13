namespace DapperDemo.Api.Infrastructure.Errors;

public static class ExceptionExtensions
{
    public static TException? GetInnerMost<TException>(this Exception exception)
        where TException : Exception
    {
        while (exception.InnerException is not null)
        {
            exception = exception.InnerException;
            if (exception is TException target) return target;
        }

        return null;
    }
}