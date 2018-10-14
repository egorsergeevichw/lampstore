using System.Collections.Generic;
using LampStore.Domain.Entities;

namespace LampStore.Domain.Models
{
    public class CartModel
    {
        public CartModel() {}

        public CartModel(CartEntity entity)
        {
            TotalPrice = entity.TotalPrice;
            CartItems = entity.CartItems.ConvertAll(x => new CartItemModel(x));
        }

        public decimal TotalPrice { get; set; }
        public List<CartItemModel> CartItems { get; set; }
    }
}
