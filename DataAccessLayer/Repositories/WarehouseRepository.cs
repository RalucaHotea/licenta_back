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
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly BoschStoreContext dbContext;
        public WarehouseRepository(BoschStoreContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<List<WarehouseEntity>> GetAllWarehouses()
        {
            return await dbContext.Warehouses.AsNoTracking().ToListAsync(); 
        }
    }
}
