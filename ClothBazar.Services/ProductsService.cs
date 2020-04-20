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

        //For Shop Conltroller
        public List<Product> SearchProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryId, int? sortBy)
        {
            using (var dbcontext = new CBContext())
            {
                var products = dbcontext.Products.ToList();

                if (categoryId.HasValue)
                {
                    products = products.Where(c => c.Id == categoryId.Value).ToList();
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }

                if (minimumPrice.HasValue)
                {
                    products = products.Where(p => p.Price >= minimumPrice.Value).ToList();
                }

                if (maximumPrice.HasValue)
                {
                    products = products.Where(p => p.Price <= maximumPrice.Value).ToList();
                }
                if(sortBy.HasValue)
                {
                    switch (sortBy)
                    {
                        case 2:
                            products = products.OrderByDescending(p => p.Id).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(p => p.Price).ToList();
                            break;
                        case 4:
                            products = products.OrderByDescending(p => p.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(p => p.Id).ToList();
                            break;

                    }
                }
                return products;
            }
        }

        public int GetMaximumPrice()
        {
            using (var dbcontext = new CBContext())
            {
              return (int)(dbcontext.Products.Max(p => p.Price));
            }
        }
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
            int pageSize = 5; /*int.Parse(ConfigurationsService.ClassObject.GetConfig("ListingPageSize").Value);*/

            using (var dbcontext = new CBContext())
            {
                return dbcontext.Products.OrderByDescending(p=>p.Id).Skip((pageNo-1)*pageSize).Take(pageSize).Include(c=>c.Category).ToList();
            }
        }

        public List<Product> GetProducts(int pageNo, int pageSize)
        {
            using (var dbcontext = new CBContext())
            {
                return dbcontext.Products.OrderByDescending(p => p.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(c => c.Category).ToList();
            }
        }

        public List<Product> GetLatestProducts(int numberofProducts)
        {

            using (var dbcontext = new CBContext())
            {
                return dbcontext.Products.OrderByDescending(p => p.Id).Take(numberofProducts).Include(c => c.Category).ToList();
            }
        }

        public List<Product> GetProductsByCategory(int CategoryId, int pageSize)
        {
            using (var dbcontext = new CBContext())
            {
                return dbcontext.Products.Where(c=>c.Category.Id==CategoryId).OrderByDescending(p => p.Id).Take(pageSize).Include(c => c.Category).ToList();
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
