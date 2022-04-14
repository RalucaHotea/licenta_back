using BusinessObjectLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Dtos
{
    public class AddOrderDto
    {
        public OrderDto Order { get; set; }
        public virtual List<CartItemEntity> Items { get; set; }

    }
}
