using Restaurant.Models;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{
    public class TableBillViewModel : ViewModelBase
    {
        List<Dish> _listItems;
        public List<Dish> ListItems
        {
            get => _listItems;
            set => SetProperty(ref _listItems, value);
        }
        public TableBillViewModel()
        {
            ListItems = new List<Dish>();
            for (int i = 0; i < 6; i++)
                ListItems.Add(new Dish
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Món " + i,
                    Description = "Quá hấp dẫn",
                    Price = 30000,
                    DishImage = "com_tam.jpg",
                });
        }
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            string v;
            parameters.TryGetValue("title", out v);
            Title = v;
            Title = "Hóa đơn " + Title.ToLower();
            return Task.CompletedTask;
        }
    }
}
