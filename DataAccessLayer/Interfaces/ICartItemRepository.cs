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
        public Task<CartItemEntity> GetItemByProductIdAsync(int productId);
        public Task DeleteCartItem(CartItemEntity item);
        public Task UpdateCartItem(CartItemEntity item);
    }
}
