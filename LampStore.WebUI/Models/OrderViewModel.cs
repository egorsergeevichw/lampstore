using LampStore.Domain.Models;

namespace LampStore.WebUI.Models
{
    public class OrderViewModel
    {
        public OrderViewModel(UserModel user, CartModel cart = null, OrderModel order = null)
        {
            User = user;
            Cart = cart ?? new CartModel();
            Order = order ?? new OrderModel();
        }
        public UserModel User { get; set; }
        public CartModel Cart { get; set; }
        public OrderModel Order { get; set; }
    }
}