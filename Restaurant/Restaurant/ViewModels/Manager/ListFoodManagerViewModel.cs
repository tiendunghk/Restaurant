using Restaurant.Datas;
using Restaurant.Models;
using Restaurant.Mvvm.Command;
using Restaurant.Services;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels.Manager
{
    public class ListFoodManagerViewModel : ViewModelBase
    {
        string _searchKeyWord;
        public string SearchKeyWord
        {
            get => _searchKeyWord;
            set
            {
                SetProperty(ref _searchKeyWord, value);
                if (string.IsNullOrEmpty(value))
                    ListFoods = BackupFood;
            }
        }
        DelegateCommand<Dish> _navigateCommand;
        DelegateCommand _searchCommand;
        public DelegateCommand<Dish> NavigateCommand => _navigateCommand ??= new DelegateCommand<Dish>(Navigate);
        DelegateCommand _addFoodCommand;
        public DelegateCommand AddFoodCommand => _addFoodCommand ??= new DelegateCommand(AddFood);
        public DelegateCommand SearchCommand => _searchCommand ??= new DelegateCommand(Search);
        List<Dish> _listFoods;
        public List<Dish> BackupFood { get; set; }
        public List<Dish> ListFoods
        {
            get => _listFoods;
            set => SetProperty(ref _listFoods, value);
        }
        bool _isSearch;
        public bool IsSearch
        {
            get => _isSearch;
            set => SetProperty(ref _isSearch, value);
        }
        public ListFoodManagerViewModel()
        {

        }
        public override async Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            var listDishs = await HttpService.GetAsync<List<Dish>>(Configuration.Api("dish/getall/true"));
            ListFoods = listDishs;
            BackupFood = ListFoods;
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
        async void Search()
        {
            IsSearch = true;
            var unicodeKeyword = Helpers.RemoveSign4VietnameseString(SearchKeyWord).ToLower();
            ListFoods = new List<Dish>();
            await Task.Delay(1000);
            IsSearch = false;
            ListFoods = new List<Dish>(BackupFood.Where(x => Helpers.RemoveSign4VietnameseString(x.Name).ToLower().Contains(unicodeKeyword)));
        }
    }
}
