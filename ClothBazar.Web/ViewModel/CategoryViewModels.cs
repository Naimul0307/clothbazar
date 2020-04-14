using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModel
{
    public class CategorySearchViewModels
    {
        public Pager Pager { get; set; }
        public List<Category> Categories { get; set; }
        public string SearchTerm { get; set; }
    }
    public class CategoryViewModels
    {
        [Required]
        [MinLength(4), MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
        public string ImageURL { get; set; }

        public bool isFeatured { get; set; }
    }
    public class EditCategoryViewModels
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4), MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public string ImageURL { get; set; }

        public bool isFeatured { get; set; }
    }
}
