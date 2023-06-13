using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthChecksPrimer.Common.Health;

/// <summary>
/// A simple demo health check returning a randomly generated result
/// </summary>
public class FooHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new())
    {
        var foo = new Random().Next(10);

        var data = new Dictionary<string, object> { { "foo", foo } };

        if (foo > 4)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy("Foo to high", data: data));
        }

        return Task.FromResult(HealthCheckResult.Healthy(data: data));
    }
}