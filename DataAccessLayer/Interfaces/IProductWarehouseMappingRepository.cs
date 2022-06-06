using BusinessObjectLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IProductWarehouseMappingRepository
    {
        public Task CreateProductWarehouseMapping(ProductWarehouseMapping productWarehouseMapping);
        public Task UpdateStock(ProductWarehouseMapping productWarehouseMapping);
        public Task<List<ProductWarehouseMapping>> GetAllStocks();
        public Task<ProductWarehouseMapping> GetAllAvailableStocksByProductId(int productId, int quantity);
        public Task<ProductWarehouseMapping> GetProductStockByProductId(int productId);
        public Task<ProductWarehouseMapping> GetProductStockByCountryAndProductId(int productId, string country);
        public Task<int> GetProductStockCountByProductId(int productId);
        public Task<ProductWarehouseMapping> GetProductStockByProductAndWarehouseId(int productId, int warehouseId);
    }
}
