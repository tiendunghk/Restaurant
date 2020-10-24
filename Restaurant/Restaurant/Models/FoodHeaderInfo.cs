using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Restaurant.Models
{
    public class FoodHeaderInfo: ObservableCollection<Dish>
    {
        public string Header { get; set; }
    }
}
