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
    public class CategoryController : Controller
    {
        CategoriesService categoriesService = new CategoriesService();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryTable(string search)
        {
            CategorySearchViewModels models = new CategorySearchViewModels();

            models.Categories = categoriesService.GetCategories();

            if (!string.IsNullOrEmpty(search))
            {
                models.SearchTerm = search;

                models.Categories = models.Categories.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
            }

            return PartialView("CategoryTable", models);
        }
        // GET: Category
        [HttpGet]
        public ActionResult Create()
        {
            CategoryViewModels models = new CategoryViewModels();

            return PartialView(models);
        }
        [HttpPost]
        public ActionResult Create(CategoryViewModels models)
        {
            {

                var newCategory = new Category();
                newCategory.Name = models.Name;
                newCategory.Description = models.Description;
                newCategory.ImageURL = models.ImageURL;
                newCategory.isFeatured = models.isFeatured;

                categoriesService.SaveCategory(newCategory);

                return RedirectToAction("CategoryTable");
            }
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            EditCategoryViewModels models = new EditCategoryViewModels();
            var category = categoriesService.GetCategory(Id);
            models.Id = category.Id;
            models.Name = category.Name;
            models.Description = category.Description;
            models.ImageURL = category.ImageURL;
            models.isFeatured = category.isFeatured;

            return PartialView(models);
        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModels models)
        {
            var existingCategory = categoriesService.GetCategory(models.Id);
            existingCategory.Name = models.Name;
            existingCategory.Description = models.Description;
            existingCategory.ImageURL = models.ImageURL;
            existingCategory.isFeatured = models.isFeatured;
            categoriesService.UpdateCategory(existingCategory);
            return RedirectToAction("CategoryTable");
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            categoriesService.DeleteCategory(Id);

            return RedirectToAction("CategoryTable");
        }

    }
}