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
        public Task<List<ProductWarehouseMapping>> GetAllStocks();
        public Task<int> GetAllStocksByProductId(int productId);
    }
}
