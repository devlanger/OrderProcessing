using Microsoft.Extensions.DependencyInjection;
using OrderProcessing.Abstractions;
using OrderProcessing.Application;
using OrderProcessing.Infrastructure;

namespace OrderProcessing;

public class Program
{
    public static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<OrderDatabase>()
            .AddTransient<ILogger, ConsoleLogger>()
            .AddScoped<IOrderService, OrderService>()
            .BuildServiceProvider();

        var databaseService = serviceProvider.GetService<OrderDatabase>();
        databaseService.SeedData();
        
        // TODO: Demonstrate multi-threaded order processing
        var orderService = serviceProvider.GetService<IOrderService>();
        
        Console.WriteLine("Order Processing System");
        
        // Example: Simulate multiple threads processing orders
        Task[] tasks = new Task[3];
        tasks[0] = Task.Run(() => { orderService.ProcessOrder(1); });
        tasks[1] = Task.Run(() => { orderService.ProcessOrder(2); });
        tasks[2] = Task.Run(() => { orderService.ProcessOrder(-1); });
        Task.WaitAll(tasks);
        
        Console.WriteLine("Processing complete.");
    }
}