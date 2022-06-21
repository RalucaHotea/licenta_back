using BusinessObjectLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface ICartService
    {
        public Task CreateCartItemAsync(CartItemDto item);
        public Task<CartItemDto> GetCartItemByIdAsync(int itemId);
        public Task<ICollection<CartItemDto>> GetCartItemsByUserIdAsync(int userId);
        public Task<ICollection<CartItemDto>> GetItemsByProductIdAsync(int productId);
        public Task UpdateCartItemAsync(CartItemDto itemToUpdate);
        public Task DeleteCartItemsByUserId(int userId);
        public Task DeleteCartItemAsync(int itemId);
    }
}
