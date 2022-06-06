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

        public async Task<OrdersStatisticsDto> GetProductsStatisticsPerYear(int year)
        {
            var allOrders = await orderService.GetAllOrdersAsync();
            var filteredOrders = allOrders.Where(x => (x.SubmittedAt ?? default).Year.Equals(year)).ToList();
            return new OrdersStatisticsDto
            {
                TotalOrdersNumber = GetTotalOrdersNumber(filteredOrders),
                InSubmissionOrdersNumber = GetInSubmissionOrdersNumber(filteredOrders),
                CompleteOrdersNumber = GetCompleteOrdersNumber(filteredOrders),
                ShippedOrdersNumber = GetShippedOrdersNumber(filteredOrders),
            };
        }

        public async Task<List<MonthlyStatisticsDto>> GetMonthlyStatistics(int year)
        {
            var allOrders = await orderService.GetAllOrdersAsync();
            var filteredOrders = allOrders.Where(x => (x.Status == OrderStatus.Complete)).ToList();
            var totalMonthlyStatistics = new List<MonthlyStatisticsDto>();
            var lastDate = new DateTime(year, 12, 31);
            var targetDate = new DateTime(year, 1, 1);
            while (targetDate <= lastDate)
            {
                var ideas = filteredOrders
                    .Where(x => ((x.PickupDate ?? default).Month.Equals(targetDate.Month)
                                 && (x.PickupDate ?? default).Year.Equals(targetDate.Year)))
                    .ToList();
                var monthData = new MonthlyStatisticsDto
                {
                    CompleteOrdersCount = ideas.Count(x => (x.PickupDate ?? default).Month.Equals(targetDate.Month)
                                 && (x.PickupDate ?? default).Year.Equals(targetDate.Year) && (x.Status == OrderStatus.Complete)),
                   
                    Date = targetDate
                };
                totalMonthlyStatistics.Add(monthData);
                targetDate = targetDate.AddMonths(1);
            }
            return totalMonthlyStatistics;
        }

        public int GetShippedOrdersNumber(ICollection<OrderDto> allOrders)
        {
            return allOrders.Count(x => x.Status == OrderStatus.Shipped);
        }

        public int GetTotalOrdersNumber(ICollection<OrderDto> allIdeas)
        {
            return allIdeas.Count(x => (x.Status == OrderStatus.InSubmission
                                   || x.Status == OrderStatus.Shipped
                                   || x.Status == OrderStatus.Complete
                                   ));
        }
    }
}
