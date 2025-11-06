using OrderProcessing.Abstractions;

namespace OrderProcessing.Application;

public class OrderService(IOrderRepository orderRepository, ILogger logger) : IOrderService, IOrderValidator
{
    public async Task ProcessOrderAsync(int orderId)
    {
        logger.LogInfo($"Start processing order: {orderId}...");

        try
        {
            if(!IsValid(orderId))
                throw new ArgumentException("Invalid order provided.");
            
            await orderRepository.GetOrderAsync(orderId);
            logger.LogInfo($"Finished processing order: {orderId}.");
        }
        catch (Exception e)
        {
            logger.LogError($"Error while processing order: {orderId}", e);
        }
    }

    public bool IsValid(int orderId)
    {
        return orderId > 0;
    }
}