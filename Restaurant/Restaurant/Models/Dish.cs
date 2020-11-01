using Newtonsoft.Json;
using Restaurant.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Models
{
    public class Dish : BindableBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public string DishImage { get; set; }
        public decimal Price { get; set; }

        int _soLuong;
        [JsonIgnore]
        public int SoLuong
        {
            get => _soLuong;
            set => SetProperty(ref _soLuong, value);
        }
    }
}
