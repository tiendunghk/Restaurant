using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Models
{
    public class OrderModel : ICloneable
    {
        public string Id { get; set; }
        public string StaffId { get; set; }
        public string TableId { get; set; }
        public DateTime OrderDate { get; set; }
        public Decimal OrderTotalAmount { get; set; }
        public DateTime OrderStart { get; set; }
        public DateTime OrderEnd { get; set; }
        public OrderStatus Status { get; set; }
        public string TableName { get; set; }
        public string IdWaiter { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
