using Newtonsoft.Json.Linq;
using Restaurant.Datas;
using Restaurant.Models;
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
    public class SplashViewModel : ViewModelBase
    {
        bool _isRunning;
        public bool IsRunning { get => _isRunning; set => SetProperty(ref _isRunning, value); }
        public override async Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            if (CheckLogin())
            {
                IsRunning = true;
                var obj = new
                {
                    StaffUsername = Preferences.Get("username", null),
                    StaffPassword = Preferences.Get("password", null)
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
                    App.Context.CurrentStaff = staff;

                    IsRunning = false;
                    Application.Current.MainPage = new AppShell();
                }
                else await DialogService.ShowAlertAsync("Vui lòng kiểm tra lại tài khoản của bạn!", "Error", "OK");
            }
            else
            {
                IsRunning = true;
                await Task.Delay(700);
                await NavigationService.NavigateToAsync<LoginViewModel>();
                IsRunning = false;
            }
        }
        bool CheckLogin()
        {
            var islogined = Preferences.Get("islogined", false);
            if (islogined)
                return true;
            return false;
        }
    }
}
