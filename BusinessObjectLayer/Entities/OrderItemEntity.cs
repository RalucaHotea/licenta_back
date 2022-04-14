using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjectLayer.Entities
{
    [Table("OrderItems")]
    public class OrderItemEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Orders")]
        public int OrderId { get; set; }

        [ForeignKey("Products")]
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public virtual OrderEntity Order { get; set; }

        public virtual ProductEntity Product { get; set; }
       


    }
}
