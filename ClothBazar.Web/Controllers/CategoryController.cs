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
        //CategoriesService categoriesService = new CategoriesService();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryTable(string search , int? pageNo)
        {
            var pageSize = ConfigurationsService.ClassObject.PageSize();
            CategorySearchViewModels models = new CategorySearchViewModels();

            models.SearchTerm = search;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = CategoriesService.ClassObject.GetCategoriesCount(search);
            models.Categories = CategoriesService.ClassObject.GetCategories(search,pageNo.Value,pageSize);

            if (models.Categories != null)
            {
                models.Pager = new Pager(totalRecords, pageNo, pageSize);
                return PartialView("CategoryTable", models);
            }
            else
            {
                return HttpNotFound();
            }
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
            if(ModelState.IsValid)
            {
                var newCategory = new Category();
                newCategory.Name = models.Name;
                newCategory.Description = models.Description;
                newCategory.ImageURL = models.ImageURL;
                newCategory.isFeatured = models.isFeatured;

                CategoriesService.ClassObject.SaveCategory(newCategory);

                return RedirectToAction("CategoryTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            EditCategoryViewModels models = new EditCategoryViewModels();
            var category = CategoriesService.ClassObject.GetCategory(Id);
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
            var existingCategory = CategoriesService.ClassObject.GetCategory(models.Id);
            existingCategory.Name = models.Name;
            existingCategory.Description = models.Description;
            existingCategory.ImageURL = models.ImageURL;
            existingCategory.isFeatured = models.isFeatured;
            CategoriesService.ClassObject.UpdateCategory(existingCategory);
            return RedirectToAction("CategoryTable");
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            CategoriesService.ClassObject.DeleteCategory(Id);

            return RedirectToAction("CategoryTable");
        }

    }
}