using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Web.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
  
    public class ShopController : Controller
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryId, int? sortBy, int? pageNo)
        {
            var pageSize = ConfigurationsService.ClassObject.PageSize();
           
            ShopViewModels models = new ShopViewModels();

            models.SearchTerm = searchTerm;
            models.FeaturedCategories = CategoriesService.ClassObject.GetFeaturedCategories();
            models.MaximumPrice = ProductsService.ClassObject.GetMaximumPrice();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            models.SortBy = sortBy;
            models.CategoryId = categoryId;
            int totalCount = ProductsService.ClassObject.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryId, sortBy);
            models.Products = ProductsService.ClassObject.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryId, sortBy,pageNo.Value,pageSize);

            models.Pager = new Pager(totalCount,pageNo,pageSize);
            return View(models);
        }

        public ActionResult FilterProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryId, int? sortBy,int?pageNo)
        {
            var pageSize = ConfigurationsService.ClassObject.PageSize();
            
            FilterProductsViewModels models = new FilterProductsViewModels();

            models.SearchTerm = searchTerm;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            models.SortBy = sortBy;
            models.CategoryId = categoryId;

            int totalCount = ProductsService.ClassObject.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryId, sortBy);
            models.Products = ProductsService.ClassObject.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryId, sortBy,pageNo.Value,pageSize);

            models.Pager = new Pager(totalCount, pageNo, pageSize);
            return PartialView(models);
        }

        [Authorize]
        public ActionResult Checkout()
        {
            CheckoutViewModels models = new CheckoutViewModels();

            var CartProductsCookie = Request.Cookies["CartProducts"];

            if (CartProductsCookie != null && !string.IsNullOrEmpty(CartProductsCookie.Value))
            {

                models.CartProductIds = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                models.CartProducts = ProductsService.ClassObject.GetProducts(models.CartProductIds);

                models.User = UserManager.FindById(User.Identity.GetUserId());
            }
            return View(models);
        }

        public JsonResult PlaceOrder(string productIds)
        {
            JsonResult result = new JsonResult();

            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (!string.IsNullOrEmpty(productIds))
            {
                var productQuantities = productIds.Split('-').Select(p => int.Parse(p)).ToList(); //productids convert array to interger

                var boughtProducts = ProductsService.ClassObject.GetProducts(productQuantities.Distinct().ToList());

                Order newOrder = new Order();
                newOrder.UserId = User.Identity.GetUserId();
                newOrder.OrderAt = DateTime.Now;
                newOrder.Status = "Pending";
                newOrder.TotalAmount = boughtProducts.Sum(p => p.Price * productQuantities.Where(productId => productId == p.Id).Count());

                newOrder.OrderItems = new List<OrderItem>();
                newOrder.OrderItems.AddRange(boughtProducts.Select(p => new OrderItem() { ProductId = p.Id, Quantity = productQuantities.Where(productId => productId == p.Id).Count() }));

                var rowsEffected = ShopService.ClassObject.SaveOrder(newOrder);

                result.Data = new { Succes= true, Rows = rowsEffected };
            }
            else
            {
                result.Data = new { Success = false };
            }

            return result;
        }
    }
}