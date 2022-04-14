using BusinessObjectLayer.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IWarehouseService
    {

        public Task CreateProductWarehouseMappingAsync(ProductWarehouseDto productWarehouse);
        //public Task UpdateProductStockAsync(ProductWarehouseDto productWarehouse);
        public Task<List<ProductWarehouseDto>> GetAllStocksAsync();
        public Task<List<WarehouseDto>> GetAllWarehousesAsync();
        public Task<List<ProductWarehouseDto>> GetAllProductStocksByIdAsync(int productId);
        public Task<int> GetProductStockCountByIdAsync(int productId);
    }
}
