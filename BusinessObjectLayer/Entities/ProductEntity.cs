using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjectLayer.Entities
{
    [Table("Products")]
    public class ProductEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string EanCode { get; set; }

        [ForeignKey("Categories")]
        public int CategoryId { get; set; }

        [ForeignKey("Subcategories")]
        public int SubcategoryId { get; set; }

        public float Price { get; set; }

        public string ImagePath { get; set; }
    }
}
