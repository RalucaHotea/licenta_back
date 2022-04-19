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

        [HttpPut]
        [Route("UpdateOrder")]
        public async Task<ActionResult> UpdateOrder([FromBody] OrderDto order)
        {
            if (order == null)
            {
                ModelState.AddModelError(string.Empty, "Order Object sent from client is null");
                return BadRequest("Order object is null");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Order object sent from client is invalid");
                return BadRequest("Order Product Object");
            }
            await orderService.UpdateOrderAsync(order);
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
        [Route("GetOrderById")]
        public async Task<ActionResult> GetOrderById([FromQuery] int orderId)
        {
            var order = await orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet]
        [Route("GetOrdersByUserOfficeLocation")]
        public async Task<ActionResult> GetOrdersByUserOfficeLocation([FromQuery] int userId)
        {
            var orders = await orderService.GetAllOrdersByUserOfficeLocationAsync(userId);
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
        [Route("GetPickupPointById")]
        public async Task<ActionResult> GetPickupPointById(int pickupPointId)
        {
            var pickupPoint = await orderService.GetPickupPointByIdAsync(pickupPointId);
            if (pickupPoint == null)
            {
                return NotFound();
            }
            return Ok(pickupPoint);
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
