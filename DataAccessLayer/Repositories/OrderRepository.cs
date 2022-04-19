using BusinessObjectLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BoschStoreContext dbContext;
        public OrderRepository(BoschStoreContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<int> CreateOrder(OrderEntity item)
        {
            dbContext.Orders.Add(item);
            await dbContext.SaveChangesAsync();
            return item.Id;
        }

        public async Task UpdateOrderAsync(OrderEntity item)
        {
            dbContext.Orders.Update(item);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateOrderItemsAsync(List<OrderItemEntity> items)
        {
            dbContext.OrderItems.UpdateRange(items);
            await dbContext.SaveChangesAsync();
        }

        public Task<List<OrderEntity>> GetAllOrders()
        {
            return dbContext.Orders.Include(x => x.User).Include(x => x.Items).ThenInclude(x => x.Product).AsNoTracking().ToListAsync();
        }

        public Task<List<OrderEntity>> GetAllOrdersByUserId(int userId)
        {
           return dbContext.Orders.Where(x => x.UserId == userId).Include(x => x.User).Include(x => x.Items).ThenInclude(x => x.Product).AsNoTracking().ToListAsync();
        }

        public Task<List<PickupPointEntity>> GetAllPickupPoints()
        {
            return dbContext.PickupPoints.AsNoTracking().ToListAsync();
        }

        public Task<PickupPointEntity> GetPickupPointById(int id)
        {
            return dbContext.PickupPoints.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<OrderEntity> GetOrderById(int orderId)
        {
            var order = await dbContext.Orders.Where(x => x.Id == orderId).Include(x => x.User).Include(x => x.Items).ThenInclude(x => x.Product).FirstOrDefaultAsync();
            return order;
        }

        public async Task<List<OrderEntity>> GetAllOrdersByUserOfficeLocationAsync(UserEntity customer)
        {
            return await dbContext.Orders.Where(x => x.PickupPoint.StreetAddress == customer.OfficeStreetAddress && x.PickupPoint.City == customer.OfficeCity && x.PickupPoint.Country == customer.OfficeCountry).ToListAsync();
        }
    }
}
