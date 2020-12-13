using Newtonsoft.Json;
using Restaurant.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Models
{
    public class OrderModel : BindableBase, ICloneable
    {
        [JsonProperty("orderId")]
        public string Id { get; set; }
        [JsonProperty("waiterId")]
        public string StaffId { get; set; }
        [JsonProperty("tableId")]
        public string TableId { get; set; }
        [JsonProperty("orderDate")]
        public DateTime? OrderDate { get; set; }
        [JsonProperty("orderTotal")]
        public Decimal OrderTotalAmount { get; set; }
        [JsonProperty("orderStarttime")]
        public DateTime? OrderStart { get; set; }
        [JsonProperty("orderEndtime")]
        public DateTime? OrderEnd { get; set; }
        [JsonProperty("orderStatus")]
        public OrderStatus Status { get => _status; set => SetProperty(ref _status, value); }
        [JsonIgnore]
        public string TableName { get; set; }
        [JsonIgnore]
        public string IdWaiter { get; set; }
        [JsonProperty("orderIsDeleted")]
        public bool IsDeleted { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
        OrderStatus _status;
    }
}
