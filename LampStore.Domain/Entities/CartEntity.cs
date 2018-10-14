using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace LampStore.Domain.Entities
{
    [Table("Carts")]
    public class CartEntity
    {
        [Key, ForeignKey("User")]
        public Guid CartId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CartItemEntity> CartItems { get; set; }
        public UserEntity User { get; set; }
    }

    public class CartEntityMap : EntityTypeConfiguration<CartEntity>
    {
        public CartEntityMap()
        {
            HasMany(p => p.CartItems).WithRequired(p => p.Cart);
        }
    }
}
