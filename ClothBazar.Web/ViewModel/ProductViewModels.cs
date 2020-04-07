using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModel
{
    public class ProductSearchViewModels
    { 
        public int PageNo { get; set; }
        public List<Product> Products { get; set; }
        public string SearchTerm { get; set; }
   
    }
    public class ProductViewModels
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public List<Category> AvailableCategories { get; set; }
    }

    public class EditProductViewModels
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public List<Category> AvailableCategories { get; set; }
    }
}