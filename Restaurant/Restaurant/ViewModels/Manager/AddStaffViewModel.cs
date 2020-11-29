using Restaurant.Datas;
using Restaurant.Models;
using Restaurant.Mvvm.Command;
using Restaurant.Services;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.ViewModels.Manager
{
    public class AddStaffViewModel : ViewModelBase
    {
        DelegateCommand _saveCommand;
        DelegateCommand _cancelCommand;
        int _selectedItem = 0;
        List<string> _roles;
        Staff _staff;
        public Staff Staff
        {
            get => _staff;
            set => SetProperty(ref _staff, value);
        }
        public List<string> Roles
        {
            get => _roles;
            set => SetProperty(ref _roles, value);
        }
        public int SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }
        public DelegateCommand SaveCommand => _saveCommand ??= new DelegateCommand(Save);
        public DelegateCommand CancelCommand => _cancelCommand ??= new DelegateCommand(Cancel);
        public AddStaffViewModel()
        {
            Roles = new List<string>();
            var count = Datas.Roles.ListRoles.Count;
            Staff = new Staff();
            foreach (var e in Datas.Roles.ListRoles)
            {
                Roles.Add(e.RoleName);
            }
        }
        async void Save()
        {
            var a = Staff;
            Staff.Role = Datas.Roles.ListRoles[SelectedItem].RoleId;
            await HttpService.PostApiAsync<object>(Configuration.Api("staff/add"), Staff);
            DialogService.ShowToast("Đã thêm thành công");
            //lúc thêm backend chưa trả về id được thêm
            await NavigationService.NavigateBackAsync();
        }
        async void Cancel()
        {
            await NavigationService.NavigateBackAsync();
        }
    }
}
