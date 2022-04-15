using BusinessObjectLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ICartItemRepository
    {
        public Task CreateCartItemAsync(CartItemEntity item);
        public Task<CartItemEntity> GetItemByIdAsync(int itemId);
        public Task<List<CartItemEntity>> GetCartItemsByUserIdAsync(int userId);
        public Task<CartItemEntity> GetItemByProductIdAndUserIdAsync(int productId, int userId);
        public Task<List<CartItemEntity>> GetItemsByProductIdAsync(int productId);
        public Task DeleteCartItems(List<CartItemEntity> cartItems);
        public Task DeleteCartItem(CartItemEntity item);
        public Task UpdateCartItem(CartItemEntity item);
    }
}
