using HealthChecksPrimer.Common.Health;
using HealthChecksPrimer.Common.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStockServices(builder.Configuration);

builder.Services.AddHealthChecks()
    .AddCheck<FooHealthCheck>("Foo check");

var app = builder.Build();

app.MapGet("/", () => $"Health Check Primer, environment={builder.Environment.EnvironmentName}");
app.MapPost("/stock/{symbol}", (string symbol, IStockService stockService) => stockService.Process(symbol));


var healthCheckOptions = new HealthCheckOptions
{
    ResponseWriter = (context, healthReport) =>
    {
        context.Response.ContentType = "application/json; charset=utf-8";
        return context.Response.WriteAsync(healthReport.ToCustomJson());
    }
};

app.MapHealthChecks("/health", healthCheckOptions);

app.Run();