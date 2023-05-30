using HealthChecksPrimer.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IStorage, StorageBlob>();
builder.Services.AddHttpClient<IStockPrices, StockPricesMockoon>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5005/");
});


var app = builder.Build();

app.MapPost("/stock/{symbol}", (string symbol, IStockService stockService) => stockService.Process(symbol) );

app.Run();