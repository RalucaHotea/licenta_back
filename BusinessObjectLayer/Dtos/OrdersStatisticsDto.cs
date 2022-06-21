using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Dtos
{
    public class OrdersStatisticsDto
    {
        public int TotalOrdersNumber { get; set; }
        public int InSubmissionOrdersNumber { get; set; }
        public int ShippedOrdersNumber { get; set; }
        public int CompleteOrdersNumber { get; set; }
    }
}
