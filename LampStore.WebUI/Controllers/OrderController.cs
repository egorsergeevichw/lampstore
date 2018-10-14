using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LampStore.Domain.Abstract;
using LampStore.Domain.Models.Requests;
using LampStore.Domain.Utils;
using LampStore.WebUI.Models;

namespace LampStore.WebUI.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        public OrderController(IOrderRepository orderRepo, IUserRepository userRepo) : base (orderRepo)
        {
            _orderRepository = orderRepo;
            _userRepository = userRepo;
        }

        #region GET

        [HttpGet]
        [Authorize]
        [Route("order/cart", Name = "CartRoute")]
        public ActionResult CartPage()
        {
            ViewBag.Title = $"{SiteName} | Cart";

            var cart = _orderRepository.GetCart(CurrentUser.UserId);

            var viewModel = new CartViewModel(cart);

            return View("Cart", viewModel);
        }

        [HttpGet]
        [Authorize]
        [Route("order/ordering", Name = "OrderRoute")]
        public ActionResult OrderPage()
        {
            ViewBag.Title = $"{SiteName} | Order";

            var user = _userRepository.GetUser(CurrentUser.UserId);
            var cart = _orderRepository.GetCart(CurrentUser.UserId);

            var viewModel = new OrderViewModel(user, cart);

            return View("Order", viewModel);
        }

        #endregion

        #region POST

        [HttpPost]
        [Route("order/addToCart")]
        public ActionResult AddToCart(AddToCartRequest request)
        {
            var userId = new Guid(request.UserId);

            if (userId == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cart = _orderRepository.AddToCart(request);

            return Json(new { cartItemsCount = cart.CartItems.Count(), totalPrice = cart.TotalPrice });
        }

        [HttpPost]
        [Route("order/deleteCartItem")]
        public ActionResult DeleteCartItem(string cartItemId)
        {
            _orderRepository.DeleteCartItem(cartItemId);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("order/make")]
        public ActionResult MakeOrder(MakeOrderRequest request)
        {
            var userId = CurrentUser.UserId;

            if (userId == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = _orderRepository.MakeOrder(userId, request);

            EmailUtils.SendUserOrderEmail(CurrentUser.FullName, CurrentUser.Email, order);
            EmailUtils.SendAdminOrderEmail();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
    }
}