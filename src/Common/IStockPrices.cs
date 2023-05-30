namespace HealthChecksPrimer.Common;

public interface IStockPrices
{
    Task<decimal> GetAsync(string symbol);
}
