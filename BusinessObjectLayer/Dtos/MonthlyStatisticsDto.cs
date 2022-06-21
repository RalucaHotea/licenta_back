using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Dtos
{
    public class MonthlyStatisticsDto
    {
        public int CompleteOrdersCount { get; set; }
        public DateTime Date { get; set; }
    }
}
