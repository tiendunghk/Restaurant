using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Models
{
    public class Dish
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public string DishImage { get; set; }
        public decimal Price { get; set; }
    }
}
