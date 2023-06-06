namespace HealthChecksPrimer.Common;

public class StorageDummy : IStorage
{
    public Task SaveAsync(DateTimeOffset dateTime, string symbol, decimal price) => Task.CompletedTask;
}
