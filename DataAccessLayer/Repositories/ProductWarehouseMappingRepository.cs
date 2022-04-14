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

        public async Task CreateProductWarehouseMapping(ProductWarehouseMapping productWarehouseMapping)
        {
            dbContext.ProductWarehouseMapping.Add(productWarehouseMapping);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<ProductWarehouseMapping>> GetAllStocks()
        {
            return await dbContext.ProductWarehouseMapping.Include(x => x.Product).Include(x => x.Warehouse).AsNoTracking().ToListAsync(); 
        }

        public async Task<ProductWarehouseMapping> GetAllAvailableStocksByProductId(int productId, int quantity)
        {
            return await dbContext.ProductWarehouseMapping.Where(x => x.ProductId == productId && x.Quantity >= quantity).Include(x => x.Warehouse).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<int> GetProductStockCountByProductId(int productId)
        {
            var stocks = await dbContext.ProductWarehouseMapping.Where(x => x.ProductId == productId).AsNoTracking().ToListAsync();
            var stockCount = 0;
            foreach(var stock in stocks)
            {
                stockCount = stockCount + stock.Quantity;
            }
            return stockCount;
        }

        public async Task UpdateStock(ProductWarehouseMapping productWarehouseMapping)
        {

            dbContext.ProductWarehouseMapping.Update(productWarehouseMapping);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ProductWarehouseMapping> GetProductStockByCountryAndProductId(int productId, string country)
        {
            return await dbContext.ProductWarehouseMapping.Where(x => x.ProductId == productId && x.Warehouse.Country == country).AsNoTracking().FirstOrDefaultAsync();

        }
    }
}
