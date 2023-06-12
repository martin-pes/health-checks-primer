namespace HealthChecksPrimer.Common.Services;

public interface IStorage
{
    Task SaveAsync(DateTimeOffset dateTime, string symbol, decimal price);
}
