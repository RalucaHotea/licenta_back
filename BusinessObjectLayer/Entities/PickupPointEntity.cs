using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjectLayer.Entities
{
    [Table("PickupPoints")]
    public class PickupPointEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
    }
}
