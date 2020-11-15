using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Restaurant
{
    public class AppContext
    {
        public Staff CurrentStaff { get; set; } = new Staff();
        public List<Table> Tables = new List<Table>();
        public int KitchenClickCount = 0;
        public int OrderClickCount = 0;
        public int TableClickCount = 0;
        public Dictionary<string, ObservableCollection<OrderDetailUI>> ListOrderDetailUI = new Dictionary<string, ObservableCollection<OrderDetailUI>>();
    }
}
