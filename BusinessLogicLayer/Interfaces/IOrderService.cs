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
        public Task<List<OrderDto>> GetAllOrdersByUserIdAsync(int userId);
        public Task<List<OrderDto>> GetAllOrdersAsync();
        public Task<List<PickupPointDto>> GetAllPickupPointsAsync();
    }
}
