using Microsoft.Extensions.DependencyInjection;
using OrderProcessing.Abstractions;
using OrderProcessing.Application;
using OrderProcessing.Infrastructure;

namespace OrderProcessing;

public class Program
{
    public static async Task Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<OrderDatabase>()
            .AddTransient<ILogger, ConsoleLogger>()
            .AddTransient<IOrderRepository, OrderRepository>()
            .AddScoped<IOrderService, OrderService>()
            .BuildServiceProvider();

        var databaseService = serviceProvider.GetService<OrderDatabase>();
        var orderService = serviceProvider.GetService<IOrderService>();

        if (orderService is null || databaseService is null)
            throw new NullReferenceException("Database Service or OrderService is null.");
        
        databaseService.SeedData();
        Console.WriteLine("Order Processing System");
        
        // Example: Simulate multiple threads processing orders
        Task[] tasks = new Task[3];
        tasks[0] = orderService.ProcessOrderAsync(1);
        tasks[1] = orderService.ProcessOrderAsync(2);
        tasks[2] = orderService.ProcessOrderAsync(-1);
        await Task.WhenAll(tasks);
        
        Console.WriteLine("Processing complete.");
    }
}