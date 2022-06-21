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
        [Route("GetCartItemById")]
        public async Task<IActionResult> GetCartItemsById([FromQuery] int itemId)
        {
            var item = await cartService.GetCartItemByIdAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpGet]
        [Route("GetCartItemsByUserId")]
        public async Task<IActionResult> GetCartItemsByUserId([FromQuery] int userId)
        {
            var items = await cartService.GetCartItemsByUserIdAsync(userId);
            if (!items.Any())
            {
                return NotFound();
            }
           
            return Ok(items);
        }

        [HttpDelete]
        [Route("DeleteCartItem")]
        public async Task<IActionResult> DeleteCartItem([FromQuery] int itemId)
        {
            await cartService.DeleteCartItemAsync(itemId);
            return Ok();
        }

        [HttpDelete]
        [Route("ClearCartByUserId")]
        public async Task<IActionResult> ClearCartByUserId([FromQuery] int userId)
        {
            await cartService.DeleteCartItemsByUserId(userId);
            return Ok();
        }
    }
}
