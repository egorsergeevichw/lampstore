using System;
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
    public class ManagementController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public ManagementController(IProductRepository productRepo, IOrderRepository orderRepo) : base (orderRepo)
        {
            _productRepository = productRepo;
            _orderRepository = orderRepo;
        }

        #region GET

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("management/products/{type}/{page}", Name = "ManagementProductsRoute")]
        public ActionResult ManagementProductsPage(int page, string type, int section, string query)
        {
            ViewBag.Title = $"{SiteName} | Products management";
            ViewBag.SearchPage = 1;

            var products = _productRepository.GetProducts(page, type);

            if (!query.IsNullOrEmpty())
            {
                products = products.Where(x => x.Name.Contains(query)).ToList();
            }

            var pagedList = products.Skip((page - 1) * PageSize).Take(PageSize).ToList();

            var viewModel = new ProductsViewModel(pagedList, products.Count, type, page, section);

            return View("Product/Products", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("management/addNewProduct", Name = "AddNewProductRoute")]
        [Route("management/editProduct", Name = "EditProductRoute")]
        public ActionResult EditProductPage(Guid? productId)
        {
            ViewBag.Title = $"{SiteName} | Add new product";

            var viewModel = new ProductViewModel(null);

            if (productId == null)
            {
                return View("Product/AddNewProduct", viewModel);
            }

            ViewBag.Title = $"{SiteName} | Edit product";

            var product = _productRepository.GetProduct(productId);

            viewModel = new ProductViewModel(product);

            return View("Product/EditProduct", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("management/orders", Name = "ManagementOrdersRoute")]
        public ActionResult ManagementOrdersPage(int page, int section)
        {
            ViewBag.Title = $"{SiteName} | Orders management";

            var orders = _orderRepository.GetOrders(null);

            var pagedList = orders.Skip((page - 1) * PageSize).Take(PageSize).ToList();

            var viewModel = new OrdersViewModel(pagedList, orders.Count, page, section);

            return View("Order/Orders", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("management/order", Name = "OrderDetailsRoute")]
        public ActionResult OrderDetailsPage(Guid orderId)
        {
            ViewBag.Title = $"{SiteName} | Order details";

            var order = _orderRepository.GetOrder(orderId);

            var viewModel = new OrderViewModel(order.User, null, order);

            return View("Order/OrderDetails", viewModel);
        }

        #endregion

        #region POST

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Route("management/saveProduct")]
        public ActionResult SaveProduct(SaveProductRequest request)
        {
            _productRepository.SaveProduct(request);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Route("management/deleteProduct")]
        public ActionResult DeleteProduct(string productId)
        {
            _productRepository.DeleteProduct(productId);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Route("management/uploadProductImage")]
        public ActionResult UploadProductImage(SaveImageRequest request)
        {
            var filePath = FilesUtils.SaveImage(request.File);

            return Json(new { id = filePath.FileId, url = filePath.Url });
        }

        #endregion
    }
}