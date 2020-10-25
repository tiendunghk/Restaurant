using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant
{
    public class AppContext : Restaurant.Mvvm.BindableBase
    {
        public List<Table> Tables = new List<Table>();
        public int KitchenClickCount = 0;
    }
}
