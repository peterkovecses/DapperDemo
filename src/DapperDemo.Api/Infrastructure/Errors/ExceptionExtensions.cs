namespace DapperDemo.Api.Infrastructure.Errors;

public static class ExceptionExtensions
{
    public static ProblemDetails ToProblem(this Exception exception, bool hideDetails = true)
    
    {
        var (type, title) = Problem.GetDefinition(StatusCodes.Status500InternalServerError);
        var detail = hideDetails ? "An error occurred while processing your request." : exception.Message;
        
        return Problem.Create(StatusCodes.Status500InternalServerError, type, title, detail);      
    }
    
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