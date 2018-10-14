using System.Linq;
using System.Net;
using System.Web.Mvc;
using Castle.Core.Internal;
using LampStore.Domain.Abstract;
using LampStore.Domain.Models.Requests;
using LampStore.Domain.Utils;
using LampStore.WebUI.Models;

namespace LampStore.WebUI.Controllers
{
    public class ContentController : BaseController
    {
        private readonly IProductRepository _productRepository;

        public ContentController(IProductRepository productRepo, IOrderRepository orderRepo) : base(orderRepo)
        {
            _productRepository = productRepo;
        }

        #region GET

        [HttpGet]
        [Route("", Name = "Default")]
        [Route("content/about", Name = "AboutRoute")]
        public ActionResult AboutPage()
        {
            ViewBag.Title = $"{SiteName} | About";
            ViewBag.CurrentUserId = CurrentUser.UserId;

            var featuredProducts = _productRepository.GetFeaturedProducts();
            var newProducts = _productRepository.GetNewProducts();

            var viewModel = new AboutViewModel(featuredProducts, newProducts);

            return View("About",viewModel);
        }

        [HttpGet]
        [Route("content/products/{type}/{page}", Name = "ProductsRoute")]
        public ActionResult ProductsPage(int page, string type, int section, string query)
        {
            ViewBag.Title = $"{SiteName} | Products | {type}";
            ViewBag.CurrentUserId = CurrentUser.UserId;

            var products = _productRepository.GetProducts(page, type);

            if (!query.IsNullOrEmpty())
            {
                products = products.Where(x => x.Name.Contains(query)).ToList();
            }

            var pagedList = products.Skip((page - 1)*PageSize).Take(PageSize).ToList();

            var viewModel = new ProductsViewModel(pagedList, products.Count, type, page, section);

            return View("Products", viewModel);
        }

        [HttpGet]
        [Route("content/feedback", Name = "FeedbackRoute")]
        public ActionResult FeedbackPage()
        {
            ViewBag.Title = $"{SiteName} | Feedback";

            return View("Feedback");
        }

        [HttpGet]
        [Route("content/search", Name = "SearchRoute")]
        public ActionResult Search(string request)
        {
            ViewBag.Title = $"{SiteName} | Search result";

            return View("Products");
        }

        #endregion

        #region POST

        [HttpPost]
        [Route("content/feedback")]
        public ActionResult Feedback(FeedbackRequest request)
        {
            EmailUtils.SendFeedbackEmail(request.Name, request.Email, request.Message);

            _productRepository.SaveFeedbackMessage(request);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
    }
}