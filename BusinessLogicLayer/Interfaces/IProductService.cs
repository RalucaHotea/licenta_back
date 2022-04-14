using BusinessObjectLayer.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IProductService
    {
        public Task<int> AddProductAsync(ProductDto product);
        public Task<ProductDto> GetProductByIdAsync(int productId);
        public Task UpdateProductAsync(ProductDto product);
        public Task DeleteProductAsync(int productId);
        public Task<List<ProductDto>> GetAllProductsAsync();
        public Task<List<ProductDto>> GetProductsByCategoryIdAsync(int categoryId);
        public Task<List<ProductDto>> GetProductsBySubcategoryIdAsync(int subcategoryId);
        public Task<List<CategoryDto>> GetAllCategoriesAsync();
        public Task<List<SubcategoryDto>> GetAllSubcategoriesAsync();
        public Task<List<SubcategoryDto>> GetAllSubcategoriesByCategoryIdAsync(int CategoryId);
        
    }
}
