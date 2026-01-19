namespace DapperDemo.Api;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<BadRequestExceptionHandler>();
        services.AddExceptionHandler<DefaultExceptionHandler>();

        services.Configure<RouteHandlerOptions>(options =>
        {
            options.ThrowOnBadRequest = true;
        });

        services.AddScoped<IDbConnection>(_ =>
        {
            var connectionString = configuration.GetConnectionString("app-db")!;
            return new SqlConnection(connectionString);
        });
        
        services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();

        return services;
    }
}