using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Entities
{
    [Table("CartItems")]
    public class CartItemEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Products")]
        public int ProductId { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public virtual ProductEntity Product{ get; set; }
    }
}
