using System;
using System.Collections.Generic;
using LampStore.Domain.Models;
using LampStore.Domain.Models.Requests;

namespace LampStore.Domain.Abstract
{
    public interface IOrderRepository
    {
        CartModel AddToCart(AddToCartRequest request);
        void DeleteCartItem(string cartItemId);
        CartModel GetCart(Guid userId);
        OrderModel MakeOrder(Guid userId, MakeOrderRequest request);
        List<OrderModel> GetOrders(Guid? userId);
        OrderModel GetOrder(Guid orderId);
    }
}
