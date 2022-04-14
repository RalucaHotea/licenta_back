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

        public async Task CreateOrderAsync(AddOrderDto orderToAdd)
        {
            var orderItemsEntities = new List<OrderItemEntity>();
            var ordertest = orderToAdd.Order;
            var items = orderToAdd.Items;
            ordertest.TotalPrice = 0;

            foreach ( CartItemEntity cartItem in items)
            {
                ordertest.TotalPrice += cartItem.Product.Price * cartItem.Quantity;
                orderItemsEntities.Add(new OrderItemEntity
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                });
            }
            ordertest.Items = orderItemsEntities;
            await orderRepository.CreateOrder(mapper.Map<OrderDto,OrderEntity>(ordertest));
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await orderRepository.GetAllOrders();
            return orders.Select(mapper.Map<OrderEntity,OrderDto>).ToList();
        }

        public async Task<List<OrderDto>> GetAllOrdersByUserIdAsync(int userId)
        {
            var orders = await orderRepository.GetAllOrdersByUserId(userId);
            return orders.Select(mapper.Map<OrderEntity, OrderDto>).ToList();
        }

        public async Task<List<PickupPointDto>> GetAllPickupPointsAsync()
        {
            var pickupPoints = await orderRepository.GetAllPickupPoints();
            return pickupPoints.Select(mapper.Map<PickupPointEntity, PickupPointDto>).ToList();
        }

       
    }
}
