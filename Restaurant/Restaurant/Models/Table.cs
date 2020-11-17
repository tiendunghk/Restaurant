using Restaurant.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Models
{
    public class Table: BindableBase
    {
        TableStatus _status;
        public string Id { get; set; }
        public string TableName { get; set; }
        public TableStatus Status { get=>_status; set=>SetProperty(ref _status,value); }

        public string TableIdOrderServing { get; set; }

        public bool TableIsDeleted { get; set; }
    }
}
