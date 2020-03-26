using ClothBazar.DataBase;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Services
{
    public class ProductsService
    {
        public Product GetProduct(int Id)
        {
            using (var dbcontext = new CBContext())
            {
                return dbcontext.Products.Find(Id);

            }
        }
        public List<Product> GetProducts()
        {
            using (var dbcontext = new CBContext())
            {
                return dbcontext.Products.ToList();

            }
        }
        public void SaveProduct(Product product)
        {
            using (var dbcontext = new CBContext())
            {
                dbcontext.Products.Add(product);
                dbcontext.SaveChanges();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var dbcontext = new CBContext())
            {
                dbcontext.Entry(product).State = System.Data.Entity.EntityState.Modified;
                dbcontext.SaveChanges();
            }
        }
        public void DeleteProduct(int Id)
        {
            using (var dbcontext = new CBContext())
            {
                //dbcontext.Entry(category).State = System.Data.Entity.EntityState.Deleted;

                var product = dbcontext.Products.Find(Id);
                dbcontext.Products.Remove(product);
                dbcontext.SaveChanges();
            }
        }
    }
}
