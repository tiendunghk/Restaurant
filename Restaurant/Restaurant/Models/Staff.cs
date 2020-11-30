using Newtonsoft.Json;
using Restaurant.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Models
{
    public class Staff : BindableBase, ICloneable
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
        public decimal StaffSalary { get; set; }
        [JsonIgnore]
        public bool StaffIsDeleted { get; set; }
        [JsonProperty("accessToken")]
        public string Token;
        public string ExternalId { get; set; }
        bool _isActive = true;
        [JsonProperty("staffIsActive")]
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }
        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
        [JsonIgnore]
        public string RoleName { get; set; }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
