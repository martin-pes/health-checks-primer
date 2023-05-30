namespace HealthChecksPrimer.Common;

public class StockPricesMockoon : IStockPrices
{
    public StockPricesMockoon(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> GetAsync(string symbol)
    {
        var response = await _httpClient.GetAsync($"stock?symbol={symbol}");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        return Decimal.Parse(result);
    }

    private readonly HttpClient _httpClient;
}
