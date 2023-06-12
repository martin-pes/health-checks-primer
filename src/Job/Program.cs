using HealthChecksPrimer.Common.Services;
using HealthChecksPrimer.Job;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddStockServices(context.Configuration);
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
