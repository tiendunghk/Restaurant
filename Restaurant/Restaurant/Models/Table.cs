using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Models
{
    public class Table
    {
        public string Id { get; set; }
        public string TableName { get; set; }
        public TableStatus Status { get; set; }
    }
}
