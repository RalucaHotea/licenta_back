using BusinessLogicLayer.Interfaces;
using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Entities;
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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService _orderService)
        {
            orderService = _orderService ;
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<ActionResult> CreateOrder([FromBody] AddOrderDto order)
        {
            if (order == null)
            {
                ModelState.AddModelError(string.Empty, "Item Object sent from client is null");
                return BadRequest("Item object is null");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Item object sent from client is invalid");
                return BadRequest("Item Product Object");
            }
            await orderService.CreateOrderAsync(order);
            return Ok();
        }

        [HttpGet]
        [Route("GetOrdersByUserId")]
        public async Task<ActionResult> GetOrdersByUserId([FromQuery] int userId)
        {
            var orders = await orderService.GetAllOrdersByUserIdAsync(userId);
            if (!orders.Any())
            {
                return NotFound();
            }
            return Ok(orders);
        }

        [HttpGet]
        [Route("GetAllOrders")]
        public async Task<ActionResult> GetAllOrders()
        {
            var orders = await orderService.GetAllOrdersAsync();
            if(!orders.Any())
            {
                return NotFound();
            }
            return Ok(orders);
        }

        [HttpGet]
        [Route("GetAllPickupPoints")]
        public async Task<ActionResult> GetAllPickupPoints()
        {
            var pickupPoints = await orderService.GetAllPickupPointsAsync();
            if (!pickupPoints.Any())
            {
                return NotFound();
            }
            return Ok(pickupPoints);
        }
    }
}
