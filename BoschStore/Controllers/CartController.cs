using BusinessLogicLayer.Interfaces;
using BusinessObjectLayer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoschStore.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService cartService;
        public CartController(ICartService _cartService)
        {
            cartService = _cartService;
        }

        [HttpPost]
        [Route("CreateUserCart")]
        public async Task<ActionResult> CreateUserCart([FromBody] UserCartDto cart)
        {
            if (cart == null)
            {
                ModelState.AddModelError(string.Empty, "Cart Object sent from client is null");
                return BadRequest("Cart object is null");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Cart object sent from client is invalid");
                return BadRequest("Cart Product Object");
            }
            await cartService.CreateCartAsync(cart);

            return Ok(cart);
        }


        [HttpPost]
        [Route("AddItemToCart")]
        public async Task<ActionResult> AddItemToCart([FromBody] CartItemDto item)
        {
            if (item == null)
            {
                ModelState.AddModelError(string.Empty, "Item Object sent from client is null");
                return BadRequest("Item object is null");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Item object sent from client is invalid");
                return BadRequest("Item Product Object");
            }
            await cartService.CreateCartItemAsync(item);

            return Ok(item);
        }

        [HttpPut]
        [Route("UpdateCartItem")]
        public async Task<IActionResult> UpdateCartItem([FromBody] CartItemDto itemToUpdate)
        {
            if (itemToUpdate == null)
            {
                return NotFound("The item was not found!");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid item object sent from client");
                return BadRequest("Invalid item object");
            }
            await cartService.UpdateCartItemAsync(itemToUpdate);
            return Ok();
        }

        [HttpGet]
        [Route("GetCartByUserId")]
        public async Task<ActionResult> GetCartById([FromQuery] int userId)
        {
            var item = await cartService.GetCartByUserIdAsync(userId);
            if (item == null)
            {
                return NotFound("The item was not found!");
            }
            return Ok(item);
        }

        [HttpDelete]
        [Route("DeleteCartItem")]
        public async Task<IActionResult> DeleteCartItem([FromQuery] int itemId)
        {
            if (itemId == 0)
            {
                ModelState.AddModelError(string.Empty, "Item object sent from client is null");
                return BadRequest("Item object is null");
            }
            await cartService.DeleteCartItemAsync(itemId);
            return Ok();
        }

        [HttpDelete]
        [Route("ClearCartByUserId")]
        public async Task<IActionResult> ClearCartByUserId([FromQuery] int userId)
        {
            if (userId == 0)
            {
                ModelState.AddModelError(string.Empty, "User object sent from client is null");
                return BadRequest("User object is null");
            }
            await cartService.ClearCartByUserIdAsync(userId);
            return Ok();
        }
    }
}
