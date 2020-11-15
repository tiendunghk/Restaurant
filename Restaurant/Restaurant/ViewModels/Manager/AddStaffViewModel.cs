using Restaurant.Models;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.ViewModels.Manager
{
    public class AddStaffViewModel : ViewModelBase
    {
        Staff _staff;
        public Staff Staff
        {
            get => _staff;
            set => SetProperty(ref _staff, value);
        }
        public AddStaffViewModel()
        {
            Staff = new Staff();
        }
    }
}
