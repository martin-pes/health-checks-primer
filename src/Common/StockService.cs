using Microsoft.Extensions.Logging;

namespace HealthChecksPrimer.Common;

public class StockService : IStockService
{
    public StockService(ILogger<StockService> log, IStockPrices stockPrices, IStorage storage)
    {
        _log = log;
        _stockPrices = stockPrices;
        _storage = storage;
    }

    public async Task<StockResult> Process(string symbol)
    {
        var dateTime = DateTimeOffset.Now;
        var price = await _stockPrices.GetAsync(symbol);
        await _storage.SaveAsync(dateTime, symbol, price);
        _log.LogInformation("{symbol} with price {price} processed", symbol, price);
        return new StockResult(symbol, price);
    }

    private readonly ILogger<StockService> _log;
    private readonly IStockPrices _stockPrices;
    private readonly IStorage _storage;
}
