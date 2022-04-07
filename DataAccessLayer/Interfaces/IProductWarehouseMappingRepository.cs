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
        public Task CreateProductWarehouseMappingAsync(ProductWarehouseMapping productWarehouseMapping);
        public Task<List<ProductWarehouseMapping>> GetAllStocksAsync();
        public int GetProductStockByProductIdAsync(int productId);

    }
}
