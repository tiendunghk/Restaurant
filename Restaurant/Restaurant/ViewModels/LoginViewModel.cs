using Acr.UserDialogs;
using Com.OneSignal;
using Newtonsoft.Json.Linq;
using Restaurant.Datas;
using Restaurant.Models;
using Restaurant.Mvvm.Command;
using Restaurant.Services;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Restaurant.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        string _userName = "4";
        string _passWord = "4";
        bool _isVisible;
        bool _hide = true;
        DelegateCommand _loginCommand;
        DelegateCommand _hideImageTapped;
        DelegateCommand _forgotPasswordCommand;
        public DelegateCommand LoginCommand => _loginCommand ??= new DelegateCommand(Login);
        public DelegateCommand HideImageTapped => _hideImageTapped ??= new DelegateCommand(Tapped);
        public DelegateCommand ForgotPasswordCommand => _forgotPasswordCommand ??= new DelegateCommand(Reset);
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
            using (UserDialogs.Instance.Loading("Login..."))
            {
                //IsVisible = true;
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
                    var listDishs = await HttpService.GetAsync<List<Dish>>(Configuration.Api($"dish/getall/true"));
                    var listRoles = await HttpService.GetAsync<List<Role>>(Configuration.Api($"role/getall"));
                    var listTables = await HttpService.GetAsync<List<Table>>(Configuration.Api($"table/getall/true"));
                    Datas.Dishs.ListDishs = new ObservableCollection<Dish>(listDishs);
                    Datas.Roles.ListRoles = listRoles;
                    Tables.ListTables = listTables;
                    foreach (var e in Datas.Roles.ListRoles)
                    {
                        if (staff.Role == e.RoleId)
                            staff.RoleName = e.RoleName;
                    }
                    var externalId = Guid.NewGuid().ToString("N");
                    OneSignal.Current.SetExternalUserId(externalId);
                    Preferences.Set("extId", externalId);
                    staff.ExternalId = Preferences.Get("extId", null);
                    await HttpService.PostApiAsync<object>(Configuration.Api("staff/update"), staff);
                    App.Context.CurrentStaff = staff;
                    Preferences.Set("islogined", true);
                    Preferences.Set("username", UserName);
                    Preferences.Set("password", PassWord);
                    Application.Current.MainPage = new AppShell();
                }
                else await DialogService.ShowAlertAsync("Vui lòng kiểm tra lại tài khoản của bạn!", "Error", "OK");
                //IsVisible = false;
            }
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
        async void Reset()
        {
            await DialogService.ShowAlertAsync("Vui lòng liên hệ quản trị viên để được giúp đỡ", "Thông báo", "OK");
        }
    }
}
