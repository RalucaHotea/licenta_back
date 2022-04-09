using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public OrderService(IOrderRepository _orderRepository, IMapper _mapper)
        {
            orderRepository = _orderRepository;
            mapper = _mapper;
        }
        public async Task<double> AddOrderAsync(OrderDto order)
        {
            var totalPrice = 0.0;

            foreach(CartItemEntity item in order.Items)
            {
                var price = item.Product.Price * item.Quantity;
                totalPrice = totalPrice + price;
            }
            var newOrder = new OrderDto
            {
                UserId = order.UserId,
                Status = order.Status,
                SubmittedAt = DateTime.Now,
                TotalPrice = totalPrice,
            };

            await orderRepository.CreateOrder(mapper.Map<OrderDto, OrderEntity>(newOrder));

            return totalPrice;
        }
    }
}
