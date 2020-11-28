using Acr.UserDialogs;
using Newtonsoft.Json;
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

namespace Restaurant.ViewModels.Manager
{
    public class StaffDetailViewModel : ViewModelBase
    {
        DelegateCommand _cancelCommand;
        DelegateCommand _saveCommand;
        public DelegateCommand CancelCommand => _cancelCommand ??= new DelegateCommand(Cancel);
        public DelegateCommand SaveCommand => _saveCommand ??= new DelegateCommand(Save);
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
        Staff _staff, _referenceObj;
        public Staff Staff
        {
            get => _staff;
            set => SetProperty(ref _staff, value);
        }
        public StaffDetailViewModel()
        {
            Roles = new List<string>();
            foreach (var r in Datas.Roles.ListRoles)
            {
                Roles.Add(r.RoleName);
            }
            SelectedItem = 0;
        }
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            parameters.TryGetValue("Staff", out _referenceObj);
            Staff = _referenceObj.Clone() as Staff;
            for (int i = 0; i < Datas.Roles.ListRoles.Count; i++)
                if (Staff.Role == Datas.Roles.ListRoles[i].RoleId)
                    SelectedItem = i;
            return Task.CompletedTask;
        }
        async void Cancel()
        {
            await NavigationService.NavigateBackAsync();
        }
        async void Save()
        {
            _referenceObj.Name = Staff.Name;
            _referenceObj.IsActive = Staff.IsActive;
            _referenceObj.Name = Staff.Name;
            _referenceObj.UserName = Staff.UserName;
            _referenceObj.StaffVisa = Staff.StaffVisa;
            _referenceObj.Role = Staff.Role = Datas.Roles.ListRoles[SelectedItem].RoleId;
            _referenceObj.StaffSalary = Staff.StaffSalary;
            var url = JsonConvert.SerializeObject(_referenceObj);
            await HttpService.PostApiAsync<object>(Configuration.Api("staff/update"), _referenceObj);
            await NavigationService.NavigateBackAsync();
        }
    }
}
