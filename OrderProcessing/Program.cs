using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderProcessing.Abstractions;
using OrderProcessing.Application.Extensions;
using OrderProcessing.Core.Config;
using OrderProcessing.Infrastructure;
using OrderProcessing.Infrastructure.Extensions;

namespace OrderProcessing;

public class Program
{
    public static async Task Main(string[] args)
    {
        //Add app settings
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        //Use DI Container
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IConfiguration>(configuration)
            .Configure<LoggingConfiguration>(opt =>
            {
                configuration.GetSection("LoggingConfiguration").Bind(opt);
            })
            .AddApplicationServices()
            .AddInfrastructureServices()
            .BuildServiceProvider();
        
        var databaseService = serviceProvider.GetService<OrderDatabase>();
        var orderService = serviceProvider.GetService<IOrderService>();
        var orderRepository = serviceProvider.GetService<IOrderRepository>();
        var processingService = serviceProvider.GetService<IProcessingService>();

        if (orderService is null || databaseService is null || orderRepository is null || processingService is null)
            throw new NullReferenceException("Database Service or OrderService is null.");
        
        databaseService.SeedData();
        Console.WriteLine("Order Processing System");

        await processingService.Process();
    }
}