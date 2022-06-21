using BusinessObjectLayer.Enums;

namespace BusinessObjectLayer.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public RoleType RoleType { get; set; }

        public double? TotalBenefit { get; set; }

        public string Group { get; set; }

        public string OfficeStreetAddress { get; set; }

        public string OfficeCity { get; set; }

        public string OfficeCountry { get; set; }
    }
}
