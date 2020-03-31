﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Entities
{
    public class Product:BaseEntity
    {
        public decimal Price { get; set; }
        public virtual Category Category { get; set; }
        //public int CategoryId { get; set; }
    }
}
