using HealthChecksPrimer.Common.Services;

namespace HealthChecksPrimer.Job;

public class Worker : BackgroundService
{
    public Worker(ILogger<Worker> logger, IStockService stockService)
    {
        _logger = logger;
        _stockService = stockService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var result = await _stockService.Process("MSFT");

            _logger.LogInformation("Worker running at: {time} for {symbol} with result={result}", DateTimeOffset.Now, result.Symbol, result.Price);

            await Task.Delay(3000, stoppingToken);
        }
    }

    private readonly ILogger<Worker> _logger;
    private readonly IStockService _stockService;
}
