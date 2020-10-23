using Restaurant.Mvvm.Command;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        DelegateCommand _loginCommand;
        public DelegateCommand LoginCommand => _loginCommand ??= new DelegateCommand(Login);
        public LoginViewModel()
        {

        }
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            return base.OnNavigationAsync(parameters, navigationType);
        }
        async void Login()
        {
            await NavigationService.NavigateToAsync<ListTableViewModel>();
        }
    }
}
