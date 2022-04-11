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
            dbContext.Items.Add(item);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteCartItem(CartItemEntity item)
        {
            dbContext.Items.Remove(item);
            await dbContext.SaveChangesAsync();
        }

        public Task<CartItemEntity> GetItemByIdAsync(int itemId)
        {
            return dbContext.Items.Where(x => x.Id == itemId).AsNoTracking().FirstOrDefaultAsync();
        }

        public Task<CartItemEntity> GetItemByProductIdAsync(int productId)
        {
            return dbContext.Items.Where(x => x.ProductId == productId).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task UpdateCartItem(CartItemEntity item)
        {
            dbContext.Items.Update(item);
            await dbContext.SaveChangesAsync();
        }
    }
}
