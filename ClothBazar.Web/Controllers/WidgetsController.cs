using ClothBazar.Services;
using ClothBazar.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class WidgetsController : Controller
    {
        // GET: Widgets
        public ActionResult Products(bool isLatestProducts, int? CategoryId = 0)
        {
            ProductsWidgetsViewModels models = new ProductsWidgetsViewModels();
            models.IsLatestProducts = isLatestProducts;

            if (isLatestProducts)
            {
                models.Products = ProductsService.ClassObject.GetLatestProducts(4);
            }

            else if(CategoryId.HasValue && CategoryId.Value>0)
            {
                models.Products = ProductsService.ClassObject.GetProductsByCategory(CategoryId.Value,4);
            }
            else
            {
                models.Products = ProductsService.ClassObject.GetProducts(1, 8);
            }
            return PartialView(models);
        }
    }
}