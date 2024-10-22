using DeliveryApp.Models;
using DeliveryApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Services.Implementations
{
    public class OrderProcessor : IOrderProcessor
    {
        private readonly DeliveryAppDbContext _deliveryAppDbContext;
        private readonly ILoggerService _loggerService;
        public OrderProcessor(DeliveryAppDbContext deliveryAppDbContext, ILoggerService loggerService) 
        {
            _deliveryAppDbContext = deliveryAppDbContext;
            _loggerService = loggerService;
        }

        public async Task<IEnumerable<Order>> FilterOrdersAsync(string cityDistrict, DateTime firstDeliveryDateTime)
        {
            try
            {
                _loggerService.Log($"Filtering orders for district: {cityDistrict} from {firstDeliveryDateTime}");

                DateTime maxDeliveryTime = firstDeliveryDateTime.AddMinutes(30);
                var filteredOrders = await _deliveryAppDbContext.Orders.Where(o => o.CityDistrict == cityDistrict && o.DeliveryTime >= firstDeliveryDateTime && o.DeliveryTime <= maxDeliveryTime).ToListAsync();

                if (!filteredOrders.Any())
                {
                    _loggerService.Log($"No orders were found!");
                    return Enumerable.Empty<Order>();
                }

                _loggerService.Log($"Found {filteredOrders.Count} orders for district {cityDistrict}");

                return filteredOrders;
            }
            catch (Exception ex) when (ex is ArgumentNullException or OperationCanceledException)
            {
                _loggerService.Log($"Error occurred: {ex.Message}");
                return Enumerable.Empty<Order>();
            }
        }

        public async Task CreateOrderAsync(Order order)
        {
            try
            {
                await _deliveryAppDbContext.Orders.AddAsync(order);
                await _deliveryAppDbContext.SaveChangesAsync();
                _loggerService.Log($"Order {order.Id} created successfully.");
            }
            catch (Exception ex) when (ex is DbUpdateException or DbUpdateConcurrencyException or OperationCanceledException)
            {
                _loggerService.Log($"Error occurred: {ex.Message}");
            }
        }
    }
}
