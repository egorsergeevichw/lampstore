using LampStore.Domain.Models;

namespace LampStore.WebUI.Models
{
    public class CartViewModel
    {
        public CartViewModel(CartModel cart)
        {
            Cart = cart;
        }
        public CartModel Cart { get; set; }
    }
}