using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Restaurant.Converters
{
    public class KitchenButtonTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var a = (OrderDetailStatus)value;
            if (a == OrderDetailStatus.WAITING)
                return "Start Cook";
            return "Call Waiter";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
