using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjectLayer.Entities
{
    [Table("CartItems")]

    public class CartItemEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }

        [ForeignKey("Products")]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public virtual ProductEntity Product { get; set; }
    }
}
