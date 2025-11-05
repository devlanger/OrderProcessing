using System.Collections.Concurrent;

namespace OrderProcessing.Infrastructure;

public class OrderDatabase
{
    public ConcurrentDictionary<int, Order> Orders { get; set; }

    public void SeedData()
    {
        Orders = new ConcurrentDictionary<int, Order>()
        {
            Values =
            {
                { new Order() { Id = 1, Description = "Laptop"} },
                { new Order() { Id = 2, Description = "Phone"} }
            }
        };
    }
}