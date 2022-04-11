using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjectLayer.Entities
{
    [Table("CartItems")]

    public class CartItemEntity
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public virtual ProductEntity Product { get; set; }
        public virtual UserCartEntity Cart { get; set; }
    }
}
