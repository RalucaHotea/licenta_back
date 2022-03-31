using BusinessObjectLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly BoschStoreContext dbContext;
        public ProductRepository(BoschStoreContext context)
        {
            dbContext = context;
        }
        public async Task<int> AddProductAsync(ProductEntity product)
        {
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();
            return product.Id;
        }

        public Task<ProductEntity> GetProductById(int productId)
        {
            return dbContext.Products.Where(x => x.Id == productId).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task UpdateProductAsync(ProductEntity product)
        {
            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(ProductEntity product)
        {
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();
        }

        public Task<List<CategoryEntity>> GetAllCategories()
        {
            return dbContext.Categories.AsNoTracking().ToListAsync();
        }
        public Task<List<SubcategoryEntity>> GetAllSubcategories()
        {
            return dbContext.Subcategories.AsNoTracking().ToListAsync();
        }

        public Task<List<ProductEntity>> GetAllProducts()
        {
            return dbContext.Products.AsNoTracking().ToListAsync();
        }

        public Task<List<ProductEntity>> GetProductsByCategoryId(int categoryId)
        {
            return dbContext.Products.Where(x => x.CategoryId == categoryId).AsNoTracking().ToListAsync();
        }
        public Task<List<ProductEntity>> GetProductsBySubcategoryId(int subcategoryId)
        {
            return dbContext.Products.Where(x => x.SubcategoryId == subcategoryId).AsNoTracking().ToListAsync();
        }

        public Task<List<SubcategoryEntity>> GetAllSubcategoriesByCategoryId(int categoryId)
        {
            return dbContext.Subcategories.Where(x => x.CategoryId == categoryId).AsNoTracking().ToListAsync();
        }
    }
}
