using Restaurant.Models;
using Restaurant.Mvvm.Command;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.ViewModels.Manager
{
    public class ListStaffManagerViewModel: ViewModelBase
    {
        DelegateCommand _navigateCommand;
        public DelegateCommand NavigateCommand => _navigateCommand ??= new DelegateCommand(Navigate);
        List<Staff> _listStaffs;
        public List<Staff> ListStaffs
        {
            get => _listStaffs;
            set => SetProperty(ref _listStaffs, value);
        }
        public ListStaffManagerViewModel()
        {
            ListStaffs = new List<Staff>()
            {
                new Staff{Id=Guid.NewGuid().ToString("N"),Name="Dũng Nguyễn",UserName="dnuit",PassWord="12345",Role="CEO"},
                new Staff{Id=Guid.NewGuid().ToString("N"),Name="Dũng Nguyễn",UserName="dnuit",PassWord="12345",Role="CEO"},
                new Staff{Id=Guid.NewGuid().ToString("N"),Name="Dũng Nguyễn",UserName="dnuit",PassWord="12345",Role="CEO"},
                new Staff{Id=Guid.NewGuid().ToString("N"),Name="Dũng Nguyễn",UserName="dnuit",PassWord="12345",Role="CEO"},
                new Staff{Id=Guid.NewGuid().ToString("N"),Name="Dũng Nguyễn",UserName="dnuit",PassWord="12345",Role="CEO"},
                new Staff{Id=Guid.NewGuid().ToString("N"),Name="Dũng Nguyễn",UserName="dnuit",PassWord="12345",Role="CEO"},
                new Staff{Id=Guid.NewGuid().ToString("N"),Name="Dũng Nguyễn",UserName="dnuit",PassWord="12345",Role="CEO"},
                new Staff{Id=Guid.NewGuid().ToString("N"),Name="Dũng Nguyễn",UserName="dnuit",PassWord="12345",Role="CEO"},
            };
        }
        async void Navigate()
        {
            await NavigationService.NavigateToAsync<AddStaffViewModel>();
        }
    }
}
