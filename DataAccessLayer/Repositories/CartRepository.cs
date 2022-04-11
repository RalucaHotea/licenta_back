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

        public async Task<int> CreateCartAsync(UserCartEntity cart)
        {
            dbContext.Carts.Add(cart);
            await dbContext.SaveChangesAsync();
            return cart.Id;
        }

        public Task<UserCartEntity> GetCartByUserIdAsync(int userId)
        {
            return dbContext.Carts.Where(x => x.UserId == userId).Include(x => x.Items).FirstOrDefaultAsync();
        }

        public Task<UserCartEntity> GetCartByIdAsync(int cartId)
        {
            return dbContext.Carts.Where(x => x.Id == cartId).Include(x => x.Items).FirstOrDefaultAsync();
        }

    }
}
