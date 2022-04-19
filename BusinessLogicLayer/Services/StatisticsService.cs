using BusinessLogicLayer.Interfaces;
using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IOrderService orderService;

        public StatisticsService(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        public int GetCompleteOrdersNumber(ICollection<OrderDto> allOrders)
        {
            return allOrders.Count(x => x.Status == OrderStatus.Complete);
        }

        public int GetInSubmissionOrdersNumber(ICollection<OrderDto> allOrders)
        {
            return allOrders.Count(x => x.Status == OrderStatus.InSubmission);
        }

        public async Task<OrdersStatisticsDto> GetProductsStatisticsDto()
        {
            var allOrders = await orderService.GetAllOrdersAsync();
            return new OrdersStatisticsDto
            {
                TotalOrdersNumber = GetTotalOrdersNumber(allOrders),
                InSubmissionOrdersNumber = GetInSubmissionOrdersNumber(allOrders),
                CompleteOrdersNumber = GetCompleteOrdersNumber(allOrders),
                ShippedOrdersNumber = GetShippedOrdersNumber(allOrders),
            };
        }

        public int GetShippedOrdersNumber(ICollection<OrderDto> allOrders)
        {
            return allOrders.Count(x => x.Status == OrderStatus.Sent);
        }

        public int GetTotalOrdersNumber(ICollection<OrderDto> allIdeas)
        {
            return allIdeas.Count(x => (x.Status == OrderStatus.InSubmission
                                   || x.Status == OrderStatus.Sent
                                   || x.Status == OrderStatus.Complete
                                   ));
        }
    }
}
