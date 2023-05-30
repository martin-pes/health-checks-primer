namespace HealthChecksPrimer.Common;

public interface IStockService
{
    Task<StockResult> Process(string symbol);
}

public record StockResult(string Symbol, decimal Price);
