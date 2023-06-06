namespace HealthChecksPrimer.Common.Services;

public interface IStockService
{
    Task<StockResult> Process(string symbol);
}

public record StockResult(string Symbol, decimal Price);
