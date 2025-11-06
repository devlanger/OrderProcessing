using OrderProcessing.Abstractions;

namespace OrderProcessing.Application;

public class ProcessingService(
    IOrderService orderService,
    IOrderRepository orderRepository,
    INotificationService notificationService) : IProcessingService
{
    public async Task Process()
    {
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
        notificationService.Send("All orders processed.");
    }
}