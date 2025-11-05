using OrderProcessing.Abstractions;

namespace OrderProcessing.Application;

public class OrderService(IOrderRepository orderRepository, ILogger logger) : IOrderService
{
    public async Task ProcessOrderAsync(int orderId)
    {
        logger.LogInfo($"Start processing order: {orderId} ...");

        try
        {
            await orderRepository.GetOrderAsync(orderId);
        }
        catch (Exception e)
        {
            logger.LogError($"Error while processing order: {orderId}", e);
            throw;
        }
    }
}