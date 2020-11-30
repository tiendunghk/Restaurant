using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Models
{
    public class OrderDetail : ICloneable
    {
        [JsonProperty("orderDetailId")]
        public string OrderDetailId { get; set; }
        [JsonProperty("dishId")]
        public string DishId { get; set; }
        [JsonProperty("orderId")]
        public string OrderDetail_OrderID { get; set; }
        [JsonProperty("cookId")]
        public string OrderDetail_CookID { get; set; }
        [JsonProperty("orderDetailStatus")]
        public OrderDetailStatus OrderDetailStatus { get; set; }
        [JsonProperty("orderDetailStarttime")]
        public DateTime OrderDetailStartTime { get; set; }
        [JsonProperty("orderDetailEndtime")]
        public DateTime OrderDetailEndTime { get; set; }
        [JsonProperty("orderDetailIsDeleted")]
        public bool IsDeleted { get; set; }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
