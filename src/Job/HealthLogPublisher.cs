using HealthChecksPrimer.Common.Health;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthChecksPrimer.Job;

public class HealthLogPublisher : IHealthCheckPublisher
{
    public HealthLogPublisher(ILogger<HealthLogPublisher> log)
    {
        _log = log;
    }

    public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
    {
        var mapping = new Dictionary<HealthStatus, LogLevel>
        {
            { HealthStatus.Healthy, LogLevel.Information },
            { HealthStatus.Degraded, LogLevel.Warning },
            { HealthStatus.Unhealthy, LogLevel.Error }
        };

        _log.Log(mapping[report.Status], "Health check completed {status}, details: {result}", report.Status, report.ToCustomJson());

        return Task.CompletedTask;
    }

    private readonly ILogger<HealthLogPublisher> _log;
}
