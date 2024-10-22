using DeliveryApp.Models;
using DeliveryApp.Services;
using DeliveryApp.Services.Implementations;
using DeliveryApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Controllers.Version1
{
    [Route("api/v1/Order/")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderProcessor _orderProcessor;
        private readonly ILoggerService _loggerService;
        public OrderController(IOrderProcessor orderProcessor, ILoggerService loggerService) 
        {
            _orderProcessor = orderProcessor;
            _loggerService = loggerService;
        }

        [HttpGet("FilterOrders")]
        public async Task<IEnumerable<Order>> FilterOrdersAsync(string cityDistrict, DateTime firstDeliveryDateTime)
        {
            return await _orderProcessor.FilterOrdersAsync(cityDistrict, firstDeliveryDateTime);
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                _loggerService.Log("Invalid order data received.");
                return BadRequest("Invalid data.");
            }

            await _orderProcessor.CreateOrderAsync(order);

            return Ok();
        }
    }
}
