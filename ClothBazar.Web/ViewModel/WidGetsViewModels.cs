using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModel
{
    public class ProductsWidgetsViewModels
    {
        public List<Product> Products { get; set; }
        public bool IsLatestProducts { get; set; }
    }
}