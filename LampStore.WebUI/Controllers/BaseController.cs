using System;
using System.Web.Mvc;
using LampStore.Domain.Abstract;
using LampStore.Domain.Models;
using LampStore.Domain.Utils;

namespace LampStore.WebUI.Controllers
{ 
    public class BaseController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public BaseController(IOrderRepository baseRepo)
        {
            _orderRepository = baseRepo;
        }

        public const string SiteName = "Classic Lamp®";
        public const int PageSize = 16;

        public UserModel CurrentUser = AuthUtils.GetCurrentUser();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.CurrentUser = CurrentUser;
            ViewBag.CartItemsCount = 0;

            if (CurrentUser != null && CurrentUser.UserId != Guid.Empty)
            {
                var cart = _orderRepository.GetCart(CurrentUser.UserId);

                ViewBag.CartItemsCount = cart.CartItems.Count;
            }
        }
    }
}