using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using LampStore.Domain.Enums;

namespace LampStore.Domain.Entities
{
    [Table("Users")]
    public class UserEntity
    {
        [Key]
        public Guid UserId { get; set; }
        public int Index { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Inn { get; set; }
        public bool ConfirmEmail { get; set; }
        public List<OrderEntity> Orders { get; set; }
        public CartEntity Cart { get; set; }
        public FeedbackEntity Feedback { get; set; }
        public UserRolesEnum Role { get; set; }
    }

    public class UserEntityMap : EntityTypeConfiguration<UserEntity>
    {
        public UserEntityMap()
        {
            HasMany(p => p.Orders).WithRequired(p => p.User);
        }
    }
}
