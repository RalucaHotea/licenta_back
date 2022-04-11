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
        public Task CreateCartAsync(UserCartDto cart);
        public Task<UserCartDto> GetCartByUserIdAsync(int userId);
        public Task CreateCartItemAsync(CartItemDto item);
        public Task<CartItemDto> GetCartItemByIdAsync(int itemId);
        public Task UpdateCartItemAsync(CartItemDto itemToUpdate);
        public Task DeleteCartItemAsync(int itemId);
        public Task ClearCartByUserIdAsync(int userId);
    }
}
