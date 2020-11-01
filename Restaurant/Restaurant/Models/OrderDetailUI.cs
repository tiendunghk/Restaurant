using Restaurant.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Models
{
    public class OrderDetailUI : BindableBase
    {
        public string OrderDetailUIId { get; set; }
        public string OrderId { get; set; }
        public OrderDetail OrderDetail { get; set; }
        public string NameDish { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public OrderDetailStatus Status { get => _status; set => SetProperty(ref _status, value); }
        public Dish Dish { get; set; }
        OrderDetailStatus _status;
        public string TableId { get; set; }
    }
}
