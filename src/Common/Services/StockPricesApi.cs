namespace HealthChecksPrimer.Common.Services;

public class StockPricesApi : IStockPrices
{
    public StockPricesApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> GetAsync(string symbol)
    {
        var response = await _httpClient.GetAsync($"stock?symbol={symbol}");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        return decimal.Parse(result);
    }

    private readonly HttpClient _httpClient;
}
