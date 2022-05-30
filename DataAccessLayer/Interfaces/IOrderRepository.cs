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
        public Task DeleteOrder(OrderEntity item);
        public Task UpdateOrderAsync(OrderEntity item);
        public Task UpdateOrderItemsAsync(List<OrderItemEntity> items);
        public Task<List<OrderEntity>> GetAllOrdersByUserOfficeLocationAsync(UserEntity customer);
        public Task<List<OrderEntity>> GetAllOrdersByUserId(int userId);
        public Task<List<OrderItemEntity>> GetAllOrderItemsByProductId(int userId);
        public Task<OrderEntity> GetOrderById(int orderId);
        public Task<List<OrderEntity>> GetAllOrders();
        public Task<List<PickupPointEntity>> GetAllPickupPoints();
        public Task<PickupPointEntity> GetPickupPointById(int id);
        public Task DeleteOrderItemAsync(OrderItemEntity item);
    }
}
