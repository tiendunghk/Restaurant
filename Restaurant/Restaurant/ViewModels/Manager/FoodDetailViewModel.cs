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
    public class FoodDetailViewModel: ViewModelBase
    {
        DelegateCommand _cancelCommand;
        DelegateCommand _saveCommand;
        Dish _obj;
        public Dish Obj
        {
            get => _obj;
            set => SetProperty(ref _obj, value);
        }
        public DelegateCommand CancelCommand => _cancelCommand ??= new DelegateCommand(Cancel);
        public DelegateCommand SaveCommand => _saveCommand ??= new DelegateCommand(Save);
        public FoodDetailViewModel()
        {

        }
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            Dish obj;
            parameters.TryGetValue("Food", out obj);
            Obj = obj;
            return Task.CompletedTask;
        }
        async void Cancel()
        {
            await NavigationService.NavigateBackAsync();
        }
        async void Save()
        {
            await NavigationService.NavigateBackAsync();
        }
    }
}
