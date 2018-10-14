using System;
using System.Linq;
using System.Web.Mvc;
using LampStore.Domain.Abstract;
using LampStore.WebUI.Models;

namespace LampStore.WebUI.Controllers
{
    public class UserController : BaseController
    {
        private readonly IOrderRepository _orderRepository;

        public UserController(IOrderRepository orderRepo) : base(orderRepo)
        {
            _orderRepository = orderRepo;
        }

        [HttpGet]
        [Route("my/orders/{section}/{page}", Name = "UserOrdersRoute")]
        public ActionResult UserOrders(int page, int section)
        {
            ViewBag.Title = $"{SiteName} | My orders";
            ViewBag.CurrentUserId = CurrentUser.UserId;

            var orders = _orderRepository.GetOrders(CurrentUser.UserId);

            var pagedList = orders.Skip((page - 1) * PageSize).Take(PageSize).ToList();

            var viewModel = new OrdersViewModel(pagedList, orders.Count, page, section);

            return View("Orders", viewModel);
        }

        [HttpGet]
        [Route("my/order/{orderId}", Name = "UserOrderDetailsRoute")]
        public ActionResult UserOrderDetails(Guid orderId)
        {
            ViewBag.Title = $"{SiteName} | Order details";

            var order = _orderRepository.GetOrder(orderId);

            var viewModel = new OrderViewModel(order.User, null, order);

            return View("OrderDetails", viewModel);
        }
    }
}