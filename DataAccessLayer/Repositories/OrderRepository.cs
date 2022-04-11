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

        public async Task CreateOrder(OrderEntity item)
        {
            dbContext.Orders.Add(item);
            await dbContext.SaveChangesAsync();

        }

        public Task<List<OrderEntity>> GetAllOrders()
        {
            return dbContext.Orders.Include(x => x.User).AsNoTracking().ToListAsync();
        }

        public Task<List<OrderEntity>> GetAllOrdersByUserId(int userId)
        {
           return dbContext.Orders.Where(x => x.UserId == userId).Include(x => x.User).AsNoTracking().ToListAsync();
        }

        public Task<List<PickupPointEntity>> GetAllPickupPoints()
        {
            return dbContext.PickupPoints.AsNoTracking().ToListAsync();
        }
    }
}
