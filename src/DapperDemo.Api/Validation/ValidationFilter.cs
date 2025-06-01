namespace DapperDemo.Api.Validation;

public class ValidationFilter<T>(IValidator<T> validator) : IEndpointFilter
    where T : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var arg = context.Arguments.OfType<T>().FirstOrDefault();
        if (arg is null) return Results.BadRequest("Request body is missing.");
        var result = await validator.ValidateAsync(arg);
        if (!result.IsValid) return Results.BadRequest(result.Errors);

        return await next(context);
    }
}