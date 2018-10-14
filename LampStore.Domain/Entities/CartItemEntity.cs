using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LampStore.Domain.Entities
{
    [Table("CartItems")]
    public class CartItemEntity
    {
        [Key]
        public Guid CartItemId { get; set; }
        public ProductEntity Product { get; set; }
        public CartEntity Cart { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
