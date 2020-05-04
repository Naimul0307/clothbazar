using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModel
{
    public class ProductSearchViewModels
    {
        public Pager Pager { get; set; }
        public List<Product> Products { get; set; }
        public string SearchTerm { get; set; }
   
    }
    public class ProductViewModels
    {
        [Required]
        [MinLength(4), MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(1, 1000000)]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string ImageURL { get; set; }
        public List<Category> AvailableCategories { get; set; }
    }

    public class EditProductViewModels
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4), MaxLength(50)]

        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(1, 1000000)]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public string ImageURL { get; set; }
        public List<Category> AvailableCategories { get; set; }
    }
    public class ProductViewModel
    {
        public Product Product { get; set; }
    }
}