using Restaurant.Models;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels.Manager
{
    public class StaffDetailViewModel : ViewModelBase
    {
        int _selectedItem;
        public int SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }
        List<string> _roles;
        public List<string> Roles
        {
            get => _roles;
            set => SetProperty(ref _roles, value);
        }
        Staff _staff;
        public Staff Staff
        {
            get => _staff;
            set => SetProperty(ref _staff, value);
        }
        public StaffDetailViewModel()
        {
            Roles = new List<string>
            {
                "Busboy","Waiter","Kitchen",
            };
            SelectedItem = 0;
        }
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            Staff s = new Staff();
            parameters.TryGetValue("Staff", out s);
            Staff = s;
            return Task.CompletedTask;
        }
    }
}
