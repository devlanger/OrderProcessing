using Microsoft.Extensions.DependencyInjection;
using OrderProcessing.Abstractions;

namespace OrderProcessing.Infrastructure.Extensions;

public static class InfrastructureCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<OrderDatabase>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        return services;
    }
}