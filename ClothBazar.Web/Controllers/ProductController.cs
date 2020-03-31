using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class ProductController : Controller
    {
        ProductsService productsService = new ProductsService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search)
        {
            var products = productsService.GetProducts();
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name == search).ToList();
            }

            return PartialView(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CategoriesService categoryService = new CategoriesService();
            var categories = categoryService.GetCategories ();
            return PartialView(categories);
        }

        [HttpPost]
        public ActionResult Create(CategoryViewModels models)
        {
            CategoriesService categoriesService = new CategoriesService();
            var newProduct = new Product();
            newProduct.Name = models.Name;
            newProduct.Description = models.Description;
            newProduct.Price = models.Price;
 
            newProduct.Category = categoriesService.GetCategory(models.CategoryId);
            productsService.SaveProduct(newProduct);
            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var product = productsService.GetProduct(Id);
            return PartialView(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            productsService.UpdateProduct(product);
            return RedirectToAction("ProductTable");
        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            productsService.DeleteProduct(Id);
            return RedirectToAction("ProductTable");
        }
    }

}