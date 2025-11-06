using Microsoft.Extensions.DependencyInjection;
using OrderProcessing.Abstractions;

namespace OrderProcessing.Application.Extensions;

public static class ApplicationCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<INotificationService, LogNotificationService>();
        services.AddTransient<ILogger, ConsoleLogger>();
        services.AddTransient<IProcessingService, ProcessingService>();
        return services;
    }
}