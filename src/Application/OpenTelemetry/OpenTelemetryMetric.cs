using System.Diagnostics.Metrics;

namespace Application.OpenTelemetry;

public static class OpenTelemetryMetric
{
    public static readonly string UserCreatedEventCounterMeterName = "UserCreatedEventCounter.Meter";
    private static readonly Meter meter = new Meter(UserCreatedEventCounterMeterName);
    
    public static Counter<int> UserCreatedEventCounter = meter.CreateCounter<int>("user.created.event.count");
    public static Histogram<int> Histogram = meter.CreateHistogram<int>("histogram.example",unit:"miliseconds");
}