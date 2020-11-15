using Restaurant.Models;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{
    public class DishDetailViewModel : ViewModelBase
    {
        Dish _obj;
        public Dish Obj
        {
            get => _obj;
            set => SetProperty(ref _obj, value);
        }

        public DishDetailViewModel()
        {

        }
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            Dish obj;
            parameters.TryGetValue("Food", out obj);
            Obj = obj;
            return Task.CompletedTask;
        }
    }
}
