namespace DapperDemo.Api;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddProblemDetails();
        
      services.AddScoped<IDbConnection>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("dapper-demo-db")!;
            
            return new NpgsqlConnection(connectionString);
        });

        return services;
    }
}