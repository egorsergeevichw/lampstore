using System;
using System.Collections.Generic;
using LampStore.Domain.Entities;
using LampStore.Domain.Enums;

namespace LampStore.Domain.Models
{
    public class UserModel
    {
        public UserModel() { }

        public UserModel(UserEntity entity)
        {
            UserId = entity.UserId;
            FullName = entity.FullName;
            CompanyName = entity.CompanyName;
            Email = entity.Email;
            PhoneNumber = entity.PhoneNumber;
            Address = entity.Address;
            Role = entity.Role;
            Inn = entity.Inn;
        }

        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Inn { get; set; }
        public UserRolesEnum Role { get; set; }
    }
}
