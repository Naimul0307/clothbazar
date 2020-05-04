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
            var pageSize = ConfigurationsService.ClassObject.PageSize();
           
            ProductSearchViewModels models = new ProductSearchViewModels();

            models.SearchTerm = search;

            pageNo = pageNo.HasValue ? pageNo.Value> 0 ? pageNo.Value :1 : 1;           //For Pagenation

            var totalRecords = ProductsService.ClassObject.GetProductsCount(search);
            models.Products = ProductsService.ClassObject.GetProducts(search, pageNo.Value,pageSize);
            models.Pager = new Pager(totalRecords, pageNo, pageSize);
            return PartialView(models);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ProductViewModels models = new ProductViewModels();
            models.AvailableCategories = CategoriesService.ClassObject.GetAllCategories();
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
            models.AvailableCategories = CategoriesService.ClassObject.GetAllCategories();

            return PartialView(models);
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModels models)
        {
            var existingProduct = ProductsService.ClassObject.GetProduct(models.Id);
            existingProduct.Name = models.Name;
            existingProduct.Description = models.Description;
            existingProduct.Price = models.Price;

            existingProduct.Category = null; //make it null because the referency key is changed
            existingProduct.CategoryId = models.CategoryId;
            
           // do not update if imageUrl is empty
            if (!string.IsNullOrEmpty(models.ImageURL))
            {
                existingProduct.ImageURL = models.ImageURL;
            }
            ProductsService.ClassObject.UpdateProduct(existingProduct);


            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Details(int Id)
        {
            ProductViewModel models = new ProductViewModel();

            models.Product = ProductsService.ClassObject.GetProduct(Id);

            return View(models);
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            ProductsService.ClassObject.DeleteProduct(Id);
            return RedirectToAction("ProductTable");
        }
    }

}