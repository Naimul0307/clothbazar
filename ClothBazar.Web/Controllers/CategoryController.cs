using ClothBazar.Entities;
using ClothBazar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class CategoryController : Controller
    {
        CategoriesService CategoriesService = new CategoriesService();
        public ActionResult Index()
        {
            var categories = CategoriesService.GetCategories();
            return View(categories);
        }
        // GET: Category
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            CategoriesService.SaveCategory(category);
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var category = CategoriesService.GetCategory(Id);
            return View(category);
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            CategoriesService.UpdateCategory(category);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var category = CategoriesService.GetCategory(Id);
            return View(category);
        }
        [HttpPost]
        public ActionResult Delete(Category category)
        {
            CategoriesService.DeleteCategory(category.Id);
            return RedirectToAction("Index");
        }

    }
}