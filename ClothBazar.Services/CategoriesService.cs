using ClothBazar.DataBase;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Services
{
    public class CategoriesService
    {
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
               return dbcontext.Categories.ToList();
               
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
    }
}
