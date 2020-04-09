using ClothBazar.DataBase;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace ClothBazar.Services
{
    public class CategoriesService
    {
        #region Singleton
        public static CategoriesService ClassObject
        {
            get
            {
                if (privetInMemoryObject == null)
                {
                    privetInMemoryObject = new CategoriesService();
                }
                return privetInMemoryObject;
            }
        }

        private static CategoriesService privetInMemoryObject { get; set; }      //Singleton Design Pattern For Pagenation
        private CategoriesService()
        {

        }
        #endregion
        public Category GetCategory(int Id)
        {
            using (var dbcontext = new CBContext())
            {
                return dbcontext.Categories.Find(Id);

            }
        }
        public List<Category> GetCategories()
        { 
            using (var dbcontext = new CBContext())
            {
               return dbcontext.Categories.OrderByDescending(c=>c.Id).Include(p=>p.Products).ToList();
               
            }
        }
        public List<Category> GetFeaturedCategories()
        {
            using (var dbcontext = new CBContext())
            {
                return dbcontext.Categories.Where(c=>c.isFeatured==true && c.ImageURL !=null).ToList();

            }
        }
        public void SaveCategory(Category category)
        {
            using(var dbcontext= new CBContext())
            {
                dbcontext.Categories.Add(category);
                dbcontext.SaveChanges();
            }
        }

        public void UpdateCategory(Category category)
        {
            using (var dbcontext = new CBContext())
            {
                dbcontext.Entry(category).State = System.Data.Entity.EntityState.Modified;
                dbcontext.SaveChanges();
            }
        }
        public void DeleteCategory(int Id)
        {
            using(var dbcontext = new CBContext())
            {
                //dbcontext.Entry(category).State = System.Data.Entity.EntityState.Deleted;

                var category = dbcontext.Categories.Find(Id);
                dbcontext.Categories.Remove(category);
                dbcontext.SaveChanges();
            }
        }
    }
}
