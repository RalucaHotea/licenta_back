using BusinessObjectLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IStatisticsService
    {
        Task<OrdersStatisticsDto> GetProductsStatisticsDto();
        int GetTotalOrdersNumber(ICollection<OrderDto> allOrders);
        int GetInSubmissionOrdersNumber(ICollection<OrderDto> allOrders);
        int GetShippedOrdersNumber(ICollection<OrderDto> allOrders);
        int GetCompleteOrdersNumber(ICollection<OrderDto> allOrders);
        Task<List<MonthlyStatisticsDto>> GetMonthlyStatistics(int year);
    }
}
