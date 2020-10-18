using Autofac;
using Restaurant.Mvvm.Command;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Restaurant.ViewModels
{
    public class Page2ViewModel:ViewModelBase
    {
        public Page2ViewModel()
        {

        }
        DelegateCommand _page1Command;
        public DelegateCommand Page1Command => _page1Command ??= new DelegateCommand(GoToPage1, () => !IsBusy)
            .ObservesProperty(() => IsBusy);
        async void GoToPage1()
        {
            NavigationParameters param= new NavigationParameters();
            param.Add("abc", "def");

            await NavigationService.NavigateToAsync<Page1ViewModel>(param);
        }
    }
}
