namespace HealthChecksPrimer.Common.Services;

public interface IStockPrices
{
    Task<decimal> GetAsync(string symbol);
}
