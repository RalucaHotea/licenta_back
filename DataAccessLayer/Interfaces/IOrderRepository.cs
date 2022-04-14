using BusinessObjectLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IOrderRepository
    {
        public Task<int> CreateOrder(OrderEntity item);
        public Task UpdateOrderAsync(OrderEntity item);
        public Task UpdateOrderItemsAsync(List<OrderItemEntity> items);
        public Task<List<OrderEntity>> GetAllOrdersByUserId(int userId);
        public Task<List<OrderEntity>> GetAllOrders();
        public Task<List<PickupPointEntity>> GetAllPickupPoints();
    }
}
