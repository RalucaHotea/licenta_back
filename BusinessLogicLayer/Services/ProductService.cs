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
        private readonly ICartRepository cartRepository;
        private readonly IProductWarehouseMappingRepository productWarehouseMappingRepository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository _productRepository, IProductWarehouseMappingRepository _productWarehouseMappingRepository, ICartRepository _cartRepository, IMapper _mapper)
        {
            productRepository = _productRepository;
            productWarehouseMappingRepository = _productWarehouseMappingRepository;
            mapper = _mapper;
            cartRepository = _cartRepository;
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
            await productWarehouseMappingRepository.CreateProductWarehouseMappingAsync(productStock);
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

        public async Task UpdateProductAsync(ProductDto productToUpdate)
        {
            await productRepository.UpdateProductAsync(mapper.Map<ProductDto, ProductEntity>(productToUpdate));
        }

        public async Task DeleteProductAsync(int productId)
        {

            var productToDelete = await productRepository.GetProductById(productId);

            if (productToDelete != null)
            {
                var folderName = Path.Combine("Resources", "Images");
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fullPath = Path.Combine(folderPath, productToDelete.Name);

                if (Directory.Exists(fullPath))
                {
                    foreach (var item in System.IO.Directory.GetFiles(fullPath))
                    {
                        System.IO.File.Delete(item);
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

        public async Task CreateCartItemAsync(CartItemDto itemToAdd)
        {
            var item = await cartRepository.GetItemByUserAndProductIdAsync(itemToAdd.UserId,itemToAdd.ProductId);
            if (item != null)
            {
                await cartRepository.UpdateCartItem(item);
            }
            else
            {
                await cartRepository.CreateCartItem(mapper.Map<CartItemDto, CartItemEntity>(itemToAdd));
            }
        }
        public async Task<CartItemDto> GetCartItemByIdAsync(int itemId)
        {
            var item = await cartRepository.GetItemByIdAsync(itemId);
            return mapper.Map<CartItemEntity, CartItemDto>(item);
        }

        public async Task<List<CartItemDto>> GetCartItemsByUserIdAsync(int userId)
        {
            var items = await cartRepository.GetCartItemsByUserIdAsync(userId);
            return items.Select(mapper.Map<CartItemEntity, CartItemDto>)
                .ToList();
        }

        public async Task UpdateCartItemAsync(CartItemDto itemToUpdate)
        {
            if(itemToUpdate.Quantity == 0)
            {
                await cartRepository.DeleteCartItem(mapper.Map<CartItemDto, CartItemEntity>(itemToUpdate));
            }
            else { 
            await cartRepository.UpdateCartItem(mapper.Map<CartItemDto, CartItemEntity>(itemToUpdate));
            }
        }

        public async Task DeleteCartItemAsync(int itemId)
        {
            var itemToDelete = await cartRepository.GetItemByIdAsync(itemId);
            if(itemToDelete != null)
            {
                await cartRepository.DeleteCartItem(itemToDelete);
            }
        }

    }
}
