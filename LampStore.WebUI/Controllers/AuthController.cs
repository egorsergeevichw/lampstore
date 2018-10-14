using System;
using System.Net;
using System.Web.Mvc;
using LampStore.Domain.Abstract;
using LampStore.Domain.Models.Requests;
using LampStore.Domain.Utils;

namespace LampStore.WebUI.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepo, IOrderRepository orderRepo) : base (orderRepo)
        {
            _userRepository = userRepo;
        }

        #region GET

        [HttpGet]
        [Route("auth/registration", Name = "RegistrationRoute")]
        public ActionResult RegistrationPage()
        {
            ViewBag.Title = $"{SiteName} | Registration";

            return View("Registration");
        }

        [HttpGet]
        [Route("auth/login", Name = "LoginRoute")]
        public ActionResult LoginPage(Guid? userId)
        {
            ViewBag.Title = $"{SiteName} | Login";
            ViewBag.IsConfirmation = false;

            if (userId != null)
            {
                _userRepository.ConfirmEmail(userId);

                ViewBag.IsConfirmation = true;
            }
            
            return View("Login");
        }

        #endregion GET

        #region POST

        [HttpPost]
        [Route("auth/registration")]
        public ActionResult Registration(RegistrationRequest request)
        {
            var userId = _userRepository.AddUser(request);

            if (userId == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
            }

            EmailUtils.SendRegistrationEmail(request.FullName, userId, request.Email);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("auth/login")]
        public ActionResult Login(LoginRequest request)
        {
            var userId = _userRepository.LoginUser(request);

            if (userId == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var cookie = AuthUtils.SetCookie(userId);
            Response.Cookies.Add(cookie);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("auth/logout")]
        public ActionResult Logout()
        {
            if (Request.Cookies[".LampStoreCookie"] != null)
            {
                var httpCookie = Response.Cookies[".LampStoreCookie"];

                if (httpCookie != null)
                    httpCookie.Expires = DateTime.Now.AddDays(-1);

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        #endregion POST
    }
}