using System.Collections.Generic;
using LampStore.Domain.Entities;

namespace LampStore.Domain.Models
{
    public class OrderItemModel
    {
        public OrderItemModel(OrderItemEntity entity)
        {
            Count = entity.Count;
            Product = new ProductModel(entity.Product);
            Price = entity.Price;
        }

        public ProductModel Product { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
