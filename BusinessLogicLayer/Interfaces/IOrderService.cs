using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IOrderService
    {
        public Task CreateOrderAsync(AddOrderDto orderToAdd);
        public Task UpdateOrderAsync(OrderDto orderToUpdate);
        public Task DeleteOrderAsync(int orderId);
        public Task<List<OrderDto>> GetAllOrdersByUserIdAsync(int userId);
        public Task<List<OrderDto>> GetAllOrdersByUserOfficeLocationAsync(int userId);
        public Task<List<OrderDto>> GetAllOrdersAsync();
        public Task<OrderDto> GetOrderByIdAsync(int orderId);
        public Task<List<PickupPointDto>> GetAllPickupPointsAsync();
        public Task<PickupPointDto> GetPickupPointByIdAsync(int pickupPointId);

    }
}
