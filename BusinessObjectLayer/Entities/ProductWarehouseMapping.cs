using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjectLayer.Entities
{
    [Table("ProductWarehouseMapping")]

    public class ProductWarehouseMapping
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Products")]
        public int ProductId { get; set; }

        [ForeignKey("Warehouses")]
        public int WarehouseId { get; set; }

        public int Quantity { get; set; }

        public virtual ProductEntity Product { get; set; }

        public virtual WarehouseEntity Warehouse { get; set; }
    }
}
