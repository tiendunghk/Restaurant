using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Models
{
    public class OrderDetail
    {
        public string OrderDetailId { get; set; }
        public string DishId { get; set; }
        public string OrderDetail_OrderID { get; set; }
        public string OrderDetail_CookID { get; set; }
        public OrderDetailStatus OrderDetailStatus { get; set; }
        public DateTime OrderDetailStartTime { get; set; }
        public DateTime OrderDetailEndTime { get; set; }

    }
}
