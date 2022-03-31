﻿using BusinessObjectLayer.Enums;

namespace BusinessObjectLayer.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public RoleType RoleType { get; set; }

        public float TotalBenefit { get; set; }

        public string Group { get; set; }

    }
}
