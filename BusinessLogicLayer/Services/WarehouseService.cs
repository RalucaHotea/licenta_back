using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IProductWarehouseMappingRepository productWarehouseRepository;
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IMapper mapper;

        public WarehouseService(IProductWarehouseMappingRepository _productWarehouseRepository, IWarehouseRepository _warehouseRepository, IMapper _mapper)
        {
            productWarehouseRepository = _productWarehouseRepository;
            mapper = _mapper;
            warehouseRepository = _warehouseRepository;
        }

        public async Task CreateProductWarehouseMappingAsync(ProductWarehouseDto productWarehouse)
        {
            await productWarehouseRepository.CreateProductWarehouseMapping(mapper.Map<ProductWarehouseDto, ProductWarehouseMapping>(productWarehouse));
        }

        public async Task<List<ProductWarehouseDto>> GetAllStocksAsync()
        {
            var stock = await productWarehouseRepository.GetAllStocks();
            return stock.Select(mapper.Map<ProductWarehouseMapping,ProductWarehouseDto>).ToList();
        
        }

        public async Task<List<ProductWarehouseDto>> GetAllProductStocksByIdAsync(int productId)
        {
            var stock = await productWarehouseRepository.GetAllStocks();
            return stock.Where(x => x.ProductId == productId).Select(mapper.Map<ProductWarehouseMapping, ProductWarehouseDto>).ToList();

        }

        public async Task<List<WarehouseDto>> GetAllWarehousesAsync()
        {
            var warehouses = await warehouseRepository.GetAllWarehouses();
            return warehouses.Select(mapper.Map<WarehouseEntity, WarehouseDto>).ToList();
        }

        public async Task<int> GetProductStockCountByIdAsync(int productId)
        {
            return await productWarehouseRepository.GetProductStockCountByProductId(productId);
        }
    }
}
