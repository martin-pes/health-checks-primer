namespace HealthChecksPrimer.Common;

public interface IStorage
{
    Task SaveAsync(DateTimeOffset dateTime, string symbol, decimal price);
}
