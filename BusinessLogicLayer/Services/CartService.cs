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
        private readonly ICartRepository cartRepository;
        private readonly ICartItemRepository cartItemRepository;
        private readonly IMapper mapper;

        public CartService(ICartRepository _cartRepository, ICartItemRepository _cartItemRepository, IMapper _mapper)
        {
            cartRepository = _cartRepository;
            cartItemRepository = _cartItemRepository;
            mapper = _mapper;
        }
        public async Task CreateCartAsync(UserCartDto cart)
        {
            await cartRepository.CreateCartAsync(mapper.Map<UserCartDto, UserCartEntity>(cart));
        }

        public async Task<UserCartDto> GetCartByUserIdAsync(int userId)
        {
            var cart = await cartRepository.GetCartByUserIdAsync(userId);
            return mapper.Map<UserCartEntity,UserCartDto>(cart);
        }

        public async Task<UserCartDto> GetCartByIdAsync(int cartId)
        {
            var cart = await cartRepository.GetCartByIdAsync(cartId);
            return mapper.Map<UserCartEntity, UserCartDto>(cart);
        }

        public async Task CreateCartItemAsync(CartItemDto itemToAdd)
        {
            var cart = await cartRepository.GetCartByIdAsync(itemToAdd.Id);
            var item = await cartItemRepository.GetItemByProductIdAsync(itemToAdd.ProductId);
            if (item != null)
            {
                item.Quantity++;
                await cartItemRepository.UpdateCartItem(item);
            }
            else
            {
                await cartItemRepository.CreateCartItemAsync(mapper.Map<CartItemDto, CartItemEntity>(itemToAdd));
                if(cart.Items== null)
                {
                    
                }
                cart.Items.Add(item);

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

        public async Task ClearCartByUserIdAsync(int userId)
        {
            var cart = await cartRepository.GetCartByUserIdAsync(userId);
            if (cart.Items.Any())
            {
                foreach (CartItemEntity item in cart.Items)
                {
                    await cartItemRepository.DeleteCartItem(item);
                }
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
    }
}
