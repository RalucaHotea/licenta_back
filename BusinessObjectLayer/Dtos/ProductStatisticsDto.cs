using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Dtos
{
    public class OrderStatisticsDto
    {
        public int AllProductsNumber { get; set; }
        public int InSubmissionNumber { get; set; }
        public int ShippedOrdersNumber { get; set; }
        public int CompleteOrdersNumber { get; set; }
        
    }
}
