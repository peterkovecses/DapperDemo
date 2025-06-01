namespace DapperDemo.Api.Logging;

public class ActivityEventLogEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var currentActivity = Activity.Current;
        currentActivity?.AddEvent(new ActivityEvent(logEvent.RenderMessage()));
    }
}