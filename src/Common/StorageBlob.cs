namespace HealthChecksPrimer.Common;

public class StorageBlob : IStorage
{
    public Task SaveAsync(DateTimeOffset dateTime, string symbol, decimal price)
    {
        return Task.CompletedTask;
    }
}
