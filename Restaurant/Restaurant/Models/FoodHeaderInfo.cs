using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Restaurant.Models
{
    public class FoodHeaderInfo : ObservableCollection<OrderDetailUI>, ICloneable
    {
        public string Header { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
