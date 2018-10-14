using System.Collections.Generic;
using LampStore.Domain.Models;

namespace LampStore.WebUI.Models
{
    public class OrdersViewModel
    {
        public OrdersViewModel(List<OrderModel> orders, int ordersCount, int ordersPage, int ordersSection)
        {
            Orders = orders;
            OrdersCount = ordersCount;
            OrdersPage = ordersPage;
            OrdersSection = ordersSection;
        }
        public List<OrderModel> Orders { get; set; }
        public int OrdersCount { get; set; }
        public int OrdersPage { get; set; }
        public int OrdersSection { get; set; }
    }
}