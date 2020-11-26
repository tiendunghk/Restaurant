using Newtonsoft.Json.Linq;
using Restaurant.Datas;
using Restaurant.Models;
using Restaurant.Mvvm.Command;
using Restaurant.Services;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Restaurant.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        string _userName = "e";
        string _passWord = "1";
        bool _isVisible;
        bool _hide = true;
        DelegateCommand _loginCommand;
        DelegateCommand _hideImageTapped;
        public DelegateCommand LoginCommand => _loginCommand ??= new DelegateCommand(Login);
        public DelegateCommand HideImageTapped => _hideImageTapped ??= new DelegateCommand(Tapped);
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }
        public string PassWord
        {
            get => _passWord;
            set => SetProperty(ref _passWord, value);
        }
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
            IsVisible = true;
            var obj = new
            {
                StaffUsername = UserName,
                StaffPassword = PassWord
            };
            var output = await HttpService.PostApiAsync<JObject>(Configuration.Api("signin"), obj);
            if (output != null)
            {
                Preferences.Set("token", output["token"].ToString());
                var userId = output["userId"].ToString();
                var staff = await HttpService.GetAsync<Staff>(Configuration.Api($"staff/{userId}"));
                App.Context.CurrentStaff = staff;
                Application.Current.MainPage = new AppShell();
            }
            else await DialogService.ShowAlertAsync("Vui lòng kiểm tra lại tài khoản của bạn!", "Error", "OK");
            IsVisible = false;
        }
        public bool Hide
        {
            get => _hide;
            set => SetProperty(ref _hide, value);
        }
        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }
        void Tapped()
        {
            Hide = !Hide;
        }
    }
}
