using Newtonsoft.Json;
using Restaurant.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Models
{
    public class Staff : BindableBase
    {
        public string Id { get; set; }

        public string Role { get; set; }

        public string StaffVisa { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public DateTime? StaffBirthdate { get; set; }

        public string StaffSalary { get; set; }

        public bool StaffIsDeleted { get; set; }

        public string Token;
        public string ExternalId { get; set; }
        bool _isActive = true;
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }
    }
}
