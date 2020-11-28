using Newtonsoft.Json;
using Restaurant.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Models
{
    public class Table: BindableBase
    {
        TableStatus _status;
        [JsonProperty("tableId")]
        public string Id { get; set; }
        [JsonProperty("tableName")]
        public string TableName { get; set; }
        [JsonProperty("tableStatus")]
        public TableStatus Status { get=>_status; set=>SetProperty(ref _status,value); }
        [JsonProperty("tableIdOrderServing")]
        public string TableIdOrderServing { get; set; }
        [JsonProperty("tableIsActive")]
        public bool TableIsActive { get; set; }
    }
}
