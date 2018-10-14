using System;
using System.Collections.Generic;
using LampStore.Domain.Entities;
using LampStore.Domain.Enums;
using LampStore.Domain.Utils;

namespace LampStore.Domain.Models
{
    public class OrderModel
    {
        public OrderModel() {}

        public OrderModel(OrderEntity entity)
        {
            OrderId = entity.OrderId;
            TotalPrice = entity.TotalPrice;
            OrderItems = entity.OrderItems.ConvertAll(x => new OrderItemModel(x));
            PurchaseDate = entity.PurchaseDate;
            Status = EnumUtils.GetEnumDescription(entity.Status);
            User = new UserModel(entity.User);           
        }

        public Guid OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Status { get; set; }
        public UserModel User { get; set; }
    }
}
