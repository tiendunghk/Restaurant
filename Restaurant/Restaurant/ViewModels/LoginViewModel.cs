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
        bool _hide;
        DelegateCommand _loginCommand;
        DelegateCommand _hideImageTapped;
        public DelegateCommand LoginCommand => _loginCommand ??= new DelegateCommand(Login);
        public DelegateCommand HideImageTapped => _hideImageTapped ??= new DelegateCommand(Tapped);
        public LoginViewModel()
        {
            //Hide = false;
        }
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            return base.OnNavigationAsync(parameters, navigationType);
        }
        async void Login()
        {
            await NavigationService.NavigateToAsync<ListTableViewModel>();
        }
        public bool Hide
        {
            get => _hide;
            set => SetProperty(ref _hide, value);
        }
        void Tapped()
        {
            Hide = !Hide;
        }
    }
}
