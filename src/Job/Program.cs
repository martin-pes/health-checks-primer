using HealthChecksPrimer.Common.Health;
using HealthChecksPrimer.Common.Services;
using HealthChecksPrimer.Job;
using Microsoft.Extensions.Diagnostics.HealthChecks;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddStockServices(context.Configuration);
        services.AddHostedService<Worker>();

        services.AddHealthChecks()
            .AddCheck<FooHealthCheck>("Foo check")
            .AddUrlGroup(new Uri("https://pcna-strobl.azurewebsites.net/stock/MSFT"))
            .AddAzureBlobStorage(o => o.ContainerName = "stocks"); // assumes BlobServiceClient registered in DI

        services.Configure<HealthCheckPublisherOptions>(options =>
        {
            options.Delay = TimeSpan.FromSeconds(2);
            options.Period = TimeSpan.FromSeconds(5);
        });

        services.AddSingleton<IHealthCheckPublisher, HealthLogPublisher>();

    })
    .Build();

host.Run();
