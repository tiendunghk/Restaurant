using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Models
{
    public class Staff
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
        public bool IsActive { get; set; }
    }
}
