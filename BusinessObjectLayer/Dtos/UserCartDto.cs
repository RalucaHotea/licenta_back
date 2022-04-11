using BusinessObjectLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Dtos
{
    public class UserCartDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<CartItemEntity> Items { get; set; }
    }
}
