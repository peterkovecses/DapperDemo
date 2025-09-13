using DapperDemo.Api.Infrastructure.Errors;

namespace DapperDemo.Api;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<BadRequestExceptionHandler>();
        services.AddExceptionHandler<UnhandledExceptionHandler>();
        
        services.AddScoped<IDbConnection>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("app-db")!;

            return new SqlConnection(connectionString);
        });
        
        services.Configure<RouteHandlerOptions>(options =>
        {
            options.ThrowOnBadRequest = true;
        });
        
        services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();

        return services;
    }
}