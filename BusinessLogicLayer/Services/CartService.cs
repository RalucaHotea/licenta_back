using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Entities;
using DataAccessLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class CartService : ICartService
    {
        private readonly ICartItemRepository cartItemRepository;
        private readonly IMapper mapper;

        public CartService(ICartItemRepository _cartItemRepository, IMapper _mapper)
        {
            cartItemRepository = _cartItemRepository;
            mapper = _mapper;
        }

        public async Task CreateCartItemAsync(CartItemDto itemToAdd)
        {
            var item = await cartItemRepository.GetItemByProductIdAndUserIdAsync(itemToAdd.ProductId, itemToAdd.UserId);
            if (item != null)
            {
                item.Quantity++;
                await cartItemRepository.UpdateCartItem(item);
            }
            else
            {
                await cartItemRepository.CreateCartItemAsync(mapper.Map<CartItemDto, CartItemEntity>(itemToAdd));
            }
        }
        public async Task<CartItemDto> GetCartItemByIdAsync(int itemId)
        {
            var item = await cartItemRepository.GetItemByIdAsync(itemId);
            return mapper.Map<CartItemEntity, CartItemDto>(item);
        }

        public async Task DeleteCartItemAsync(int itemId)
        {
            var itemToDelete = await cartItemRepository.GetItemByIdAsync(itemId);
            if (itemToDelete != null)
            {
                await cartItemRepository.DeleteCartItem(itemToDelete);
            }
        }

        public async Task UpdateCartItemAsync(CartItemDto itemToUpdate)
        {
            if (itemToUpdate.Quantity == 0)
            {
                await cartItemRepository.DeleteCartItem(mapper.Map<CartItemDto, CartItemEntity>(itemToUpdate));
            }
            else
            {
                await cartItemRepository.UpdateCartItem(mapper.Map<CartItemDto, CartItemEntity>(itemToUpdate));
            }
        }

        public async Task<ICollection<CartItemDto>> GetCartItemsByUserIdAsync(int userId)
        {
            var items = await cartItemRepository.GetCartItemsByUserIdAsync(userId);
            return items
                .Select(mapper.Map<CartItemEntity, CartItemDto>)
                .ToList();
        }

        public async Task<ICollection<CartItemDto>> GetItemsByProductIdAsync(int productId)
        {
            var items = await cartItemRepository.GetItemsByProductIdAsync(productId);
            return items
                .Select(mapper.Map<CartItemEntity, CartItemDto>)
                .ToList();
        }

        public async Task DeleteCartItemsByUserId(int userId)
        {
            var items = await cartItemRepository.GetCartItemsByUserIdAsync(userId);
            if (items.Any())
            {
                await cartItemRepository.DeleteCartItems(items);
            }
        }
    }
}
