using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using LampStore.Domain.Enums;

namespace LampStore.Domain.Entities
{
    [Table("Products")]
    public class ProductEntity
    {
        [Key]
        public Guid ProductId { get; set; }
        public int Index { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public DateTime AddingDate { get; set; }
        public int? Rating { get; set; }
        public ProductTypeEnum Type { get; set; }
        public List<OrderItemEntity> OrderItems { get; set; }
        public List<CartItemEntity> CartItems { get; set; }

        public class ProductEntityMap : EntityTypeConfiguration<ProductEntity>
        {
            public ProductEntityMap()
            {
                HasMany(p => p.OrderItems).WithRequired(p => p.Product);

                HasMany(p => p.CartItems).WithRequired(p => p.Product);
            }
        }
    }
}
