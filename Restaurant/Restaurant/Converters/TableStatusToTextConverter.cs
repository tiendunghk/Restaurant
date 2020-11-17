using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Restaurant.Converters
{
    public class TableStatusToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var xxx = (TableStatus)value;
            switch (xxx)
            {
                case TableStatus.OCCUPIED:
                    return "Dirty";
                case TableStatus.DIRTY:
                    return "Clean";
                default:
                    return "Occupied";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
