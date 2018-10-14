using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LampStore.Domain.Entities
{
    [Table("OrderItems")]
    public class OrderItemEntity
    {
        [Key]
        public Guid OrderItemId { get; set; }
        public ProductEntity Product { get; set; }
        public OrderEntity Order { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
