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
        //ProductsService productsService = new ProductsService();
        //CategoriesService categoryService = new CategoriesService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search, int ? pageNo)
        {
           
            ProductSearchViewModels models = new ProductSearchViewModels();

            models.PageNo = pageNo.HasValue ? pageNo.Value> 0 ? pageNo.Value :1 : 1;           //For Pagenation

            //if (pageNo.HasValue)
            //{
            //    if (pageNo.Value > 0)
            //    {
            //        models.PageNo = pageNo.Value;
            //    }
            //    else
            //    {
            //        models.PageNo = 1;
            //    }
            //}
            //else
            //{
            //    models.PageNo = 1;
            //}

            models.Products = ProductsService.ClassObject.GetProducts(models.PageNo);

            if (string.IsNullOrEmpty(search)==false)
            {
                models.SearchTerm = search;
                models.Products = models.Products.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
            }

            return PartialView(models);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ProductViewModels models = new ProductViewModels();
            models.AvailableCategories = CategoriesService.ClassObject.GetCategories();
            return PartialView(models);
        }

        [HttpPost]
        public ActionResult Create(ProductViewModels models)
        {
           
            var newProduct = new Product();
            newProduct.Name = models.Name;
            newProduct.Description = models.Description;
            newProduct.Price = models.Price;
            newProduct.ImageURL = models.ImageURL;
            newProduct.Category = CategoriesService.ClassObject.GetCategory(models.CategoryId);
            ProductsService.ClassObject.SaveProduct(newProduct);
            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            EditProductViewModels models = new EditProductViewModels();

            var product = ProductsService.ClassObject.GetProduct(Id);
            models.Id = product.Id;
            models.Name = product.Name;
            models.Description = product.Description;
            models.Price = product.Price;
            models.CategoryId = product.Category != null ? product.Category.Id : 0;
            models.ImageURL = product.ImageURL;
            models.AvailableCategories = CategoriesService.ClassObject.GetCategories();

            return PartialView(models);
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModels models)
        {
            var existingProduct = ProductsService.ClassObject.GetProduct(models.Id);
            existingProduct.Name = models.Name;
            existingProduct.Description = models.Description;
            existingProduct.Price = models.Price;
            existingProduct.Category = CategoriesService.ClassObject.GetCategory(models.CategoryId);
            existingProduct.ImageURL = models.ImageURL;
            ProductsService.ClassObject.UpdateProduct(existingProduct);


            return RedirectToAction("ProductTable");
        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            ProductsService.ClassObject.DeleteProduct(Id);
            return RedirectToAction("ProductTable");
        }
    }

}