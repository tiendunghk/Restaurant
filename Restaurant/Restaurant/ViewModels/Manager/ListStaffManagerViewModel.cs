using Restaurant.Models;
using Restaurant.Mvvm.Command;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.ViewModels.Manager
{
    public class ListStaffManagerViewModel: ViewModelBase
    {
        DelegateCommand _navigateCommand;
        public DelegateCommand NavigateCommand => _navigateCommand ??= new DelegateCommand(AddNavigate);
        DelegateCommand<Staff> _tappedCommand;
        public DelegateCommand<Staff> TappedCommand => _tappedCommand ??= new DelegateCommand<Staff>(Tapped);
        List<Staff> _listStaffs;
        public List<Staff> ListStaffs
        {
            get => _listStaffs;
            set => SetProperty(ref _listStaffs, value);
        }
        public ListStaffManagerViewModel()
        {
            ListStaffs = Datas.Staffs.ListStaffs;
        }
        async void AddNavigate()
        {
            await NavigationService.NavigateToAsync<AddStaffViewModel>();
        }
        async void Tapped(Staff obj)
        {
            var parameters = new NavigationParameters();
            parameters.Add("Staff", obj);
            await NavigationService.NavigateToAsync<StaffDetailViewModel>(parameters);
        }
    }
}
