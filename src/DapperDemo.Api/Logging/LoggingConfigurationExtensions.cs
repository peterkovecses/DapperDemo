namespace DapperDemo.Api.Logging;

public static class LoggingConfigurationExtensions
{
    public static WebApplicationBuilder ConfigureLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .Enrich.With(new ActivityEventLogEnricher())
            .CreateLogger();
        builder.Host.UseSerilog(logger);

        return builder;
    }
}