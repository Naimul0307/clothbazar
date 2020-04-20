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
      public ActionResult Index(string searchTerm , int? minimumPrice, int? maximumPrice, int? categoryId,int? sortBy)
        {
            ShopViewModels models = new ShopViewModels();

            models.FeaturedCategories = CategoriesService.ClassObject.GetFeaturedCategories();
            models.MaximumPrice = ProductsService.ClassObject.GetMaximumPrice();

            models.Products = ProductsService.ClassObject.SearchProducts(searchTerm,minimumPrice,maximumPrice,categoryId,sortBy);
            models.SortBy = sortBy;
            return View(models);
        }

      //ProductsService productsService = new ProductsService();
        public ActionResult Checkout()
        {
            CheckoutViewModels models = new CheckoutViewModels();
            var CartProductsCookie = Request.Cookies["CartProducts"];
            if (CartProductsCookie != null)
            {
                //    var productIds = CartProductsCookie.Value;
                //    var ids = productIds.Split('-');
                //    List<int> pIds = ids.Select(x => int.Parse(x)).ToList();
               
               models.CartProductsIds= CartProductsCookie.Value.Split('-').Select(p => int.Parse(p)).ToList();

                models.CartProducts = ProductsService.ClassObject.GetProducts(models.CartProductsIds);
            }
            return View(models);
        }
    }
}