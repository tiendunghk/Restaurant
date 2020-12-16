using Newtonsoft.Json;
using Restaurant.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Models
{
    public class Dish : BindableBase, ICloneable
    {
        [JsonProperty("dishId")]
        public string Id { get; set; }
        [JsonProperty("dishName")]
        public string Name { get; set; }
        [JsonProperty("dishDescription")]
        public string Description { get; set; }
        [JsonProperty("dishIsActive")]
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }
        [JsonProperty("dishImage")]
        public string DishImage
        {
            get => _dishImage;
            set => SetProperty(ref _dishImage, value);
        }
        [JsonProperty("dishPrice")]
        public decimal Price { get; set; }

        int _soLuong;
        [JsonIgnore]
        public int SoLuong
        {
            get => _soLuong;
            set
            {
                SetProperty(ref _soLuong, value);
            }
        }
        string _dishImage;
        bool _isActive = true;

        decimal _totalAmount;
        [JsonIgnore]
        public decimal TotalAmount { get => _totalAmount; set => SetProperty(ref _totalAmount, value); }
        public object Clone()
        {
            return MemberwiseClone();
        }
        public bool Validate()
        {
            if (string.IsNullOrEmpty(Name)) return false;
            if (string.IsNullOrEmpty(Description)) return false;
            if (Price == 0) return false;
            if (string.IsNullOrEmpty(DishImage)) return false;
            return true;
        }
    }
}
