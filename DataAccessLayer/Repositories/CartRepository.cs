using BusinessObjectLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly BoschStoreContext dbContext;
        public CartRepository(BoschStoreContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task CreateCartItem(CartItemEntity item)
        {
            dbContext.Items.Add(item);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteCartItem(CartItemEntity item)
        {
            dbContext.Items.Remove(item);
            await dbContext.SaveChangesAsync();
        }

        public Task<List<CartItemEntity>> GetCartItemsByUserIdAsync(int userId)
        {
            return dbContext.Items.Where(x => x.UserId == userId).Include(x => x.Product).ToListAsync();
        }

        public Task<CartItemEntity> GetItemByIdAsync(int itemId)
        {
            return dbContext.Items.Where(x => x.Id == itemId).AsNoTracking().FirstOrDefaultAsync();
        }

        public Task<CartItemEntity> GetItemByUserAndProductIdAsync(int userId, int productId)
        {
            return dbContext.Items.Where(x => x.ProductId == productId && x.UserId == userId).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task UpdateCartItem(CartItemEntity item)
        {
            dbContext.Items.Update(item);
            await dbContext.SaveChangesAsync();
        }
    }
}
