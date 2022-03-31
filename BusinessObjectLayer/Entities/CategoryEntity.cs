using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjectLayer.Entities
{
    [Table("Categories")]
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
