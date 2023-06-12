using Azure.Identity;
using HealthChecksPrimer.Common.Services;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddHttpClient<IStockPrices, StockPricesApi>(client =>
{
    client.BaseAddress = new Uri("https://pcna-strobl.azurewebsites.net");
});

builder.Services.AddScoped<IStorage, StorageBlob>();
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration.GetSection("Storage"));

    // if you want to use managed identity when running in VS and the Azure account configured in your VS (Tools->Options->Azure Service Authentication)
    // has access to multiple tenants then the tenant to use to access the storage account should be specified here
    var tenantId = builder.Configuration["VisualStudioTenantId"];
    var credentials = tenantId != null ? new DefaultAzureCredential(new DefaultAzureCredentialOptions { VisualStudioTenantId = tenantId }) : new DefaultAzureCredential();
    clientBuilder.UseCredential(credentials);
});

var app = builder.Build();

app.MapGet("/", () => $"Health Check Primer, environment={builder.Environment.EnvironmentName}");
app.MapPost("/stock/{symbol}", (string symbol, IStockService stockService) => stockService.Process(symbol));

app.Run();