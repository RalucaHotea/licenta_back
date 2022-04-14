using BusinessObjectLayer.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IWarehouseService
    {

        public Task CreateProductWarehouseMappingAsync(ProductWarehouseDto productWarehouse);
        public Task<List<ProductWarehouseDto>> GetAllStocksAsync();
        public Task<List<WarehouseDto>> GetAllWarehousesAsync();
        public Task<int> GetProductStockByIdAsync(int productId);

    }
}
