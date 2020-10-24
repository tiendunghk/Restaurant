using Restaurant.Models;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.ViewModels.Order
{
    public class OrderDetailViewModel: ViewModelBase
    {
        List<Dish> _tests;
        public List<Dish> Tests
        {
            get => _tests;
            set => SetProperty(ref _tests, value);
        }
        public OrderDetailViewModel()
        {
            Tests = new List<Dish>();
            for (int i = 0; i < 10; i++)
                Tests.Add(new Dish
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Món " + i,
                    Description = "Quá hấp dẫn",
                    Price = 30000,
                    DishImage = "com_tam.jpg",
                });
        }
    }
}
