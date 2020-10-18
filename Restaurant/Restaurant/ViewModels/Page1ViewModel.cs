using Restaurant.Mvvm.Command;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{
    public class Page1ViewModel:ViewModelBase
    {
        public Page1ViewModel()
        {
            So1 = 10;
            So2 = 20;
        }
        DelegateCommand _page2Command;
        public DelegateCommand Page2Command => _page2Command ??= new DelegateCommand(GoToPage2,()=>!IsBusy)
            .ObservesProperty(()=>IsBusy);
        DelegateCommand _congSo1;
        public DelegateCommand CongSo1 => _congSo1 ??= new DelegateCommand(()=>So1++, () => !IsBusy)
            .ObservesProperty(() => IsBusy);
        async void GoToPage2()
        {
            await NavigationService.NavigateToAsync<Page2ViewModel>();
        }
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            return base.OnNavigationAsync(parameters, navigationType);
        }
        int _so1;
        public int So1
        {
            get => _so1;
            set
            {
                SetProperty(ref _so1, value);
            }
        }
        int _so2;
        public int So2
        {
            get => _so2;
            set
            {
                SetProperty(ref _so2, value);
            }
        }
    }
}
