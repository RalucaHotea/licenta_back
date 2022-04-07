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

    public class ProductWarehouseMappingRepository : IProductWarehouseMappingRepository
    {
        private readonly BoschStoreContext dbContext;

        public ProductWarehouseMappingRepository(BoschStoreContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateProductWarehouseMappingAsync(ProductWarehouseMapping productWarehouseMapping)
        {
            dbContext.ProductWarehouseMapping.Add(productWarehouseMapping);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<ProductWarehouseMapping>> GetAllStocksAsync()
        {
            return await dbContext.ProductWarehouseMapping.AsNoTracking().ToListAsync(); 
        }

        public int GetProductStockByProductIdAsync(int productId)
        {
            return dbContext.ProductWarehouseMapping.Count(x => x.ProductId == productId);

        }
    }
}
