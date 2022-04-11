using BusinessObjectLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IOrderService
    {
        public Task<double> AddOrderAsync(OrderDto order);
        public Task<List<OrderDto>> GetAllOrdersByUserIdAsync(int userId);
        public Task<List<OrderDto>> GetAllOrdersAsync();
        public Task<List<PickupPointDto>> GetAllPickupPointsAsync();
    }
}
