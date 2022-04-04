using BusinessObjectLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IProductRepository
    {
        public Task<int> AddProductAsync(ProductEntity product);
        public Task UpdateProductAsync(ProductEntity product);
        public Task DeleteProductAsync(ProductEntity product);
        public Task<ProductEntity> GetProductById(int productId);
        public Task<List<ProductEntity>> GetAllProducts();
        public Task<List<ProductEntity>> GetProductsByCategoryId(int categoryId);
        public Task<List<ProductEntity>> GetProductsBySubcategoryId(int subcategoryId);
        public Task<List<CategoryEntity>> GetAllCategories();
        public Task<List<SubcategoryEntity>> GetAllSubcategories();
        public Task<List<SubcategoryEntity>> GetAllSubcategoriesByCategoryId(int categoryId);
    }
}
