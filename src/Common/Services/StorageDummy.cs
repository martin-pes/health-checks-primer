namespace HealthChecksPrimer.Common.Services;

public class StorageDummy : IStorage
{
    public Task SaveAsync(DateTimeOffset dateTime, string symbol, decimal price) => Task.CompletedTask;
}
