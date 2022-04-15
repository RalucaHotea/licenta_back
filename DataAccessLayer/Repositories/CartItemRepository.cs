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
    public class CartItemRepository: ICartItemRepository
    {
        private readonly BoschStoreContext dbContext;
        public CartItemRepository(BoschStoreContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task CreateCartItemAsync(CartItemEntity item)
        {
            dbContext.CartItems.Add(item);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteCartItem(CartItemEntity item)
        {
            dbContext.CartItems.Remove(item);
            await dbContext.SaveChangesAsync();

        }

        public async Task DeleteCartItems(List<CartItemEntity> cartItems)
        {
             dbContext.CartItems.RemoveRange(cartItems);
             await dbContext.SaveChangesAsync();
        }

        public Task<CartItemEntity> GetItemByIdAsync(int itemId)
        {
            return dbContext.CartItems.Where(x => x.Id == itemId).AsNoTracking().FirstOrDefaultAsync();
        }

        public Task<CartItemEntity> GetItemByProductIdAndUserIdAsync(int productId, int userId)
        {
            return dbContext.CartItems.Where(x => x.ProductId == productId && x.UserId == userId).AsNoTracking().FirstOrDefaultAsync();
        }

        public Task<List<CartItemEntity>> GetCartItemsByUserIdAsync(int userId)
        {
            return dbContext.CartItems.Where(x => x.UserId == userId).Include(x => x.Product).AsNoTracking().ToListAsync();
        }

        public async Task UpdateCartItem(CartItemEntity item)
        {
            dbContext.CartItems.Update(item);
            await dbContext.SaveChangesAsync();
        }
        public Task<List<CartItemEntity>> GetItemsByProductIdAsync(int productId)
        {
           return dbContext.CartItems.Where(x => x.ProductId == productId).ToListAsync();

        }
    }
}
