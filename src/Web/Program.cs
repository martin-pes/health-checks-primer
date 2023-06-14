using HealthChecksPrimer.Common.Health;
using HealthChecksPrimer.Common.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStockServices(builder.Configuration);

builder.Services.AddHealthChecks()
    .AddCheck<FooHealthCheck>("Foo check", tags: new[] {"monitor"})
    .AddUrlGroup(new Uri("https://pcna-strobl.azurewebsites.net/stock/MSFT"))
    .AddAzureBlobStorage(o => o.ContainerName = "stocks"); // assumes BlobServiceClient registered in DI

var app = builder.Build();

app.MapGet("/", () => $"Health Check Primer, environment={builder.Environment.EnvironmentName}");
app.MapPost("/stock/{symbol}", (string symbol, IStockService stockService) => stockService.Process(symbol));

static Task GetHealthCheckWriter(HttpContext context, HealthReport healthReport)
{
    context.Response.ContentType = "application/json; charset=utf-8";
    return context.Response.WriteAsync(healthReport.ToCustomJson());
}

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = GetHealthCheckWriter
});

app.MapHealthChecks("/health/monitor", new HealthCheckOptions
{
    ResponseWriter = GetHealthCheckWriter,
    Predicate = healthCheck => healthCheck.Tags.Contains("monitor")
});

app.Run();