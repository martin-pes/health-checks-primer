using HealthChecksPrimer.Common.Health;
using HealthChecksPrimer.Common.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStockServices(builder.Configuration);

builder.Services.AddHealthChecks()
    .AddCheck<FooHealthCheck>("Foo check");

var app = builder.Build();

app.MapGet("/", () => $"Health Check Primer, environment={builder.Environment.EnvironmentName}");
app.MapPost("/stock/{symbol}", (string symbol, IStockService stockService) => stockService.Process(symbol));
app.MapHealthChecks("/health");

app.Run();