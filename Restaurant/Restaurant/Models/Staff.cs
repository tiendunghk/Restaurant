using Newtonsoft.Json;
using Restaurant.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Models
{
    public class Staff : BindableBase
    {
        [JsonProperty("staffId")]
        public string Id { get; set; }
        [JsonProperty("roleId")]
        public string Role { get; set; }
        [JsonProperty("staffVisa")]
        public string StaffVisa { get; set; }
        [JsonProperty("staffName")]
        public string Name { get; set; }
        [JsonProperty("staffUsername")]
        public string UserName { get; set; }
        [JsonProperty("staffPassword")]
        public string PassWord { get; set; }
        [JsonProperty("staffBirthdate")]
        public DateTime? StaffBirthdate { get; set; }
        [JsonProperty("staffSalary")]
        public string StaffSalary { get; set; }
        [JsonProperty("staffIsActive")]
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
