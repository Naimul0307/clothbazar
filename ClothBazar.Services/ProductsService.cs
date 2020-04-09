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
    public class ProductsService
    {
        #region Singleton
        public static ProductsService ClassObject
        {
            get
            {
                if (privetInMemoryObject == null)
                {
                    privetInMemoryObject = new ProductsService();
                }
                return privetInMemoryObject;
            }
        }

        private static ProductsService privetInMemoryObject { get; set; }      //Singleton Design Pattern For Pagenation
        private ProductsService()
        {

        }
        #endregion
        public Product GetProduct(int Id)
        {
            using (var dbcontext = new CBContext())
            {
                return dbcontext.Products.Where(p=>p.Id==Id).Include(c=>c.Category).FirstOrDefault();

            }
        }

        public List<Product> GetProducts(List<int> Ids)
        {
            using (var dbcontext = new CBContext())
            {
                return dbcontext.Products.Where(product =>Ids.Contains(product.Id)).ToList();

            }
        }

        public List<Product> GetProducts(int pageNo)
        {
            int pageSize = 5;

            using (var dbcontext = new CBContext())
            {
                return dbcontext.Products.OrderByDescending(p=>p.Id).Skip((pageNo-1)*pageSize).Take(pageSize).Include(c=>c.Category).ToList();
            }
        }
        public void SaveProduct(Product product)
        {
            using (var dbcontext = new CBContext())
            {

                dbcontext.Entry(product).State = System.Data.Entity.EntityState.Unchanged;
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
