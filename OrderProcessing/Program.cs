using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using OrderProcessing.Abstractions;
using OrderProcessing.Application;
using OrderProcessing.Core.Config;
using OrderProcessing.Infrastructure;

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
            .AddSingleton<OrderDatabase>()
            .AddTransient<ILogger, ConsoleLogger>()
            .AddTransient<IOrderRepository, OrderRepository>()
            .AddScoped<IOrderService, OrderService>()
            .BuildServiceProvider();

        
        var databaseService = serviceProvider.GetService<OrderDatabase>();
        var orderService = serviceProvider.GetService<IOrderService>();
        var orderRepository = serviceProvider.GetService<IOrderRepository>();

        if (orderService is null || databaseService is null || orderRepository is null)
            throw new NullReferenceException("Database Service or OrderService is null.");
        
        databaseService.SeedData();
        Console.WriteLine("Order Processing System");
        
        var tasks = new Task[3];
        tasks[0] = orderService.ProcessOrderAsync(1);
        tasks[1] = orderService.ProcessOrderAsync(2);
        tasks[2] = orderService.ProcessOrderAsync(-1);
        await Task.WhenAll(tasks);
        
        orderRepository.AddOrder(new Order()
        {
            Id = 3, 
            Description = "Printer"
        });
        
        Console.WriteLine("All orders processed.");
        
        
    }
}