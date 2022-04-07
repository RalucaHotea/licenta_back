using BusinessObjectLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ICartRepository
    {
        public Task CreateCartItem(CartItemEntity item);
        public Task<CartItemEntity> GetItemByIdAsync(int itemId);
        public Task<CartItemEntity> GetItemByUserAndProductIdAsync(int userId,int productId);
        public Task<List<CartItemEntity>> GetCartItemsByUserIdAsync(int userId);
        public Task DeleteCartItem(CartItemEntity item);
        public Task UpdateCartItem(CartItemEntity item);
    }
}
