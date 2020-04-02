using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModel
{
    public class CheckoutViewModels
    {
        public List<Product> CartProducts { get; set; }
        public List<int> CartProductsIds { get; set; }
    }
}