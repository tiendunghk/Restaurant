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
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }
        public string DishImage
        {
            get => _dishImage;
            set => SetProperty(ref _dishImage, value);
        }
        public decimal Price { get; set; }

        int _soLuong;
        [JsonIgnore]
        public int SoLuong
        {
            get => _soLuong;
            set => SetProperty(ref _soLuong, value);
        }
        string _dishImage;
        bool _isActive = true;
    }
}
