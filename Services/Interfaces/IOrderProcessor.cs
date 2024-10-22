using DeliveryApp.Models;

namespace DeliveryApp.Services.Interfaces
{
    public interface IOrderProcessor
    {
        Task<IEnumerable<Order>> FilterOrdersAsync(string cityDistrict, DateTime firstDeliveryDateTime);
        Task CreateOrderAsync(Order order);
    }
}
