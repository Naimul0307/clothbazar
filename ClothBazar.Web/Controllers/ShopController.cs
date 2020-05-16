using ClothBazar.Services;
using ClothBazar.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
  
    public class ShopController : Controller
    {
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

        public ActionResult Checkout()
        {
            CheckoutViewModels models = new CheckoutViewModels();

            var CartProductsCookie = Request.Cookies["CartProducts"];

            if (CartProductsCookie != null && !string.IsNullOrEmpty(CartProductsCookie.Value))
            {

                models.CartProductIds = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                models.CartProducts = ProductsService.ClassObject.GetProducts(models.CartProductIds);
            }
            return View(models);
        }
    }
}