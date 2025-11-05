using OrderProcessing.Abstractions;

namespace OrderProcessing.Application;

public class OrderService(IOrderRepository orderRepository, ILogger logger) : IOrderService
{
    public Task ProcessOrder(int orderId)
    {
        logger.LogInfo($"Start processing order: {orderId} ...");

        try
        {
            orderRepository.GetOrder(orderId);
        }
        catch (Exception e)
        {
            logger.LogError($"Error while processing order: {orderId}", e);
            throw;
        }
        
        return Task.CompletedTask;
    }
}