using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using LampStore.Domain.Enums;

namespace LampStore.Domain.Entities
{
    [Table("Orders")]
    public class OrderEntity
    {
        [Key]
        public Guid OrderId { get; set; }
        public int Index { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public OrderStatusEnum Status { get; set; }
        public List<OrderItemEntity> OrderItems { get; set; }
        public UserEntity User { get; set; }
    }

    public class OrderEntityMap : EntityTypeConfiguration<OrderEntity>
    {
        public OrderEntityMap()
        {
            HasMany(p => p.OrderItems).WithRequired(p => p.Order);
        }
    }
}
