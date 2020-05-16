using ClothBazar.Entities;
using System;
using ClothBazar.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModel
{
    public class CheckoutViewModels
    {
        public List<Product> CartProducts { get; set; }
        public List<int> CartProductIds { get; set; }
    }

    public class ShopViewModels
    {
        public int MaximumPrice { get; set; }
        public List<Category> FeaturedCategories { get; set; }
        public List<Product> Products { get; set; }
        public int? SortBy { get;  set; }
        public int? CategoryId { get; set; }
        public Pager Pager { get; set; }
        public string SearchTerm { get; set; }
    }

    public class FilterProductsViewModels
    {
        public List<Product> Products { get; set; }
        public int? SortBy { get; set; }
        public int? CategoryId { get; set; }
        public Pager Pager { get; set; }
        public string SearchTerm { get; set; }
    }

}