using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Azure.Identity;
using Microsoft.Extensions.Azure;

namespace HealthChecksPrimer.Common.Services;
public static class ServicesDiExtensions
{
    public static IServiceCollection AddStockServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<IStockService, StockService>();
        services.AddHttpClient<IStockPrices, StockPricesApi>(client =>
        {
            client.BaseAddress = new Uri("https://pcna-strobl.azurewebsites.net");
        });

        services.AddTransient<IStorage, StorageBlob>();
        services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddBlobServiceClient(config.GetSection("Storage"));

            // if you want to use managed identity when running in VS and the Azure account configured in your VS (Tools->Options->Azure Service Authentication)
            // has access to multiple tenants then the tenant to use to access the storage account should be specified here
            var tenantId = config["VisualStudioTenantId"];
            var credentials = tenantId != null ? new DefaultAzureCredential(new DefaultAzureCredentialOptions { VisualStudioTenantId = tenantId }) : new DefaultAzureCredential();
            clientBuilder.UseCredential(credentials);
        });

        return services;
    }
}
