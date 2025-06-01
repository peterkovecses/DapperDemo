namespace DapperDemo.Api;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<CustomExceptionHandler>();
        
        services.AddScoped<IDbConnection>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("app-db")!;

            return new SqlConnection(connectionString);
        });
        
        services.Configure<RouteHandlerOptions>(options =>
        {
            options.ThrowOnBadRequest = true; // Ez a kulcsfontosságú beállítás
        });
        
        services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();

        return services;
    }
}