using BusinessObjectLayer.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjectLayer.Entities
{
    [Table("Users")]
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Username { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public RoleType RoleType { get; set; }

        [Required]
        public double? TotalBenefit { get; set; }

        public string Group { get; set; }

        public string OfficeStreetAddress { get; set; }

        public string OfficeCity { get; set; }

        public string OfficeCountry { get; set; }
    }
}
