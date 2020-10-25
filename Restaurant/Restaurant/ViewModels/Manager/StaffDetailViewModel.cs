﻿using Restaurant.Models;
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
        Staff _staff;
        public Staff Staff
        {
            get => _staff;
            set => SetProperty(ref _staff, value);
        }
        public StaffDetailViewModel()
        {

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