using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Entities;
using DataAccessLayer.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{

    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IProductWarehouseMappingRepository productWarehouseMappingRepository;
        private readonly ICartItemRepository cartItemRepository;
        private readonly IProductWarehouseMappingRepository warehouseMappingRepository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository _productRepository, IProductWarehouseMappingRepository _productWarehouseMappingRepository, ICartItemRepository _cartItemRepository, IProductWarehouseMappingRepository _warehouseRepository, IMapper _mapper)
        {
            productRepository = _productRepository;
            productWarehouseMappingRepository = _productWarehouseMappingRepository;
            cartItemRepository = _cartItemRepository;
            warehouseMappingRepository = _warehouseRepository;
            mapper = _mapper;
        }

        public async Task<int> AddProductAsync(ProductDto productToAdd)
        {
            var product = new ProductDto
            {
                Name = productToAdd.Name,
                Description = productToAdd.Description,
                EanCode = productToAdd.EanCode,
                MinimumQuantity = productToAdd.MinimumQuantity,
                CategoryId = productToAdd.CategoryId,
                SubcategoryId = productToAdd.SubcategoryId,
                Price = productToAdd.Price,
                ImagePath = productToAdd.ImagePath,

            };
            var productEntity = mapper.Map<ProductDto, ProductEntity>(product);
            var productId = await productRepository.AddProductAsync(productEntity);
            var productStock = new ProductWarehouseMapping
            {
                ProductId = productId,
                WarehouseId = productToAdd.WarehouseId,
                Quantity = productToAdd.Quantity,
            };
            await productWarehouseMappingRepository.CreateProductWarehouseMapping(productStock);
            return productId;
        }

        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            var product = await productRepository.GetProductById(productId);
            return mapper.Map<ProductEntity, ProductDto>(product);

        }

        public async Task<List<ProductDto>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var products = await productRepository.GetProductsByCategoryId(categoryId);
            return products
                .Select(mapper.Map<ProductEntity, ProductDto>)
                .ToList();
        }

        public async Task<List<ProductDto>> GetProductsBySubcategoryIdAsync(int subcategoryId)
        {
            var products = await productRepository.GetProductsBySubcategoryId(subcategoryId);
            return products
                .Select(mapper.Map<ProductEntity, ProductDto>)
                .ToList();
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await productRepository.GetAllProducts();
            return products
                .Select(mapper.Map<ProductEntity, ProductDto>)
                .ToList();
        }

        public async Task<List<ProductDto>> GetAllAvailableProductsAsync()
        {
            var products = await productRepository.GetAllProducts();
            var availableProducts = new List<ProductEntity>();
            foreach (var product in products)
            {
                var productStock = await productWarehouseMappingRepository.GetProductStockCountByProductId(product.Id);
                if (productStock != 0)
                {
                    availableProducts.Add(product);
                }
            }
            return availableProducts
                .Select(mapper.Map<ProductEntity, ProductDto>)
                .ToList();
        }

        public async Task UpdateProductAsync(ProductDto productToUpdate)
        {
            await productRepository.UpdateProductAsync(mapper.Map<ProductDto, ProductEntity>(productToUpdate));
        }

        public async Task DeleteProductAsync(int productId)
        {

            var productToDelete = await productRepository.GetProductById(productId);

            if (productToDelete != null)
            {
                var productName = productToDelete.Name.Trim();
                var folderName = Path.Combine("Resources", "Images").Trim('"');
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName).Trim('"');
                var fullPath = Path.Combine(folderPath, productName);

                if (Directory.Exists(fullPath))
                {
                    foreach (var item in Directory.GetFiles(fullPath))
                    {
                       File.Delete(item);
                    }
                }
                var cartItems = await cartItemRepository.GetItemsByProductIdAsync(productId);
                if (cartItems.Any())
                {
                    foreach( var item in cartItems)
                    {
                       await cartItemRepository.DeleteCartItem(item);
                    }
                }
                await productRepository.DeleteProductAsync(productToDelete);
            }
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await productRepository.GetAllCategories();
            return categories
                .Select(mapper.Map<CategoryEntity, CategoryDto>)
                .ToList();
        }

        public async Task<List<SubcategoryDto>> GetAllSubcategoriesAsync()
        {
            var subcategories = await productRepository.GetAllSubcategories();
            return subcategories
                .Select(mapper.Map<SubcategoryEntity, SubcategoryDto>)
                .ToList();
        }

        public async Task<List<SubcategoryDto>> GetAllSubcategoriesByCategoryIdAsync(int categoryId)
        {
            var subCategories = await productRepository.GetAllSubcategoriesByCategoryId(categoryId);
            return subCategories
                .Select(mapper.Map<SubcategoryEntity, SubcategoryDto>)
                .ToList();
        }
    }
}
