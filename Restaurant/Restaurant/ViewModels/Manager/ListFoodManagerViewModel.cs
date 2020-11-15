using Restaurant.Models;
using Restaurant.Mvvm.Command;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels.Manager
{
    public class ListFoodManagerViewModel : ViewModelBase
    {
        DelegateCommand<Dish> _navigateCommand;
        public DelegateCommand<Dish> NavigateCommand => _navigateCommand ??= new DelegateCommand<Dish>(Navigate);
        DelegateCommand _addFoodCommand;
        public DelegateCommand AddFoodCommand => _addFoodCommand ??= new DelegateCommand(AddFood);
        List<Dish> _listFoods;
        public List<Dish> ListFoods
        {
            get => _listFoods;
            set => SetProperty(ref _listFoods, value);
        }
        public ListFoodManagerViewModel()
        {
            ListFoods = Datas.Dishs.ListDishs;
        }
        async void AddFood()
        {
            await NavigationService.NavigateToAsync<AddFoodViewModel>();
        }
        async void Navigate(Dish obj)
        {
            var parameters = new NavigationParameters();
            parameters.Add("Food", obj);
            await NavigationService.NavigateToAsync<FoodDetailViewModel>(parameters);
        }
    }
}
