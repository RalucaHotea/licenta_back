using BusinessObjectLayer.Entities;
using DataAccessLayer.Interfaces;
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
    }
}
