using System.Collections.Generic;
using LampStore.Domain.Entities;

namespace LampStore.Domain.Models
{
    public class CartItemModel
    {
        public CartItemModel(CartItemEntity entity)
        {
            Count = entity.Count;
            Product = new ProductModel(entity.Product);
            Price = entity.Price;
            CartItemId = entity.CartItemId.ToString();
        }

        public ProductModel Product { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public string CartItemId { get; set; }
    }
}
