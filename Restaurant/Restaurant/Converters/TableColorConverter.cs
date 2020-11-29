using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Restaurant.Converters
{
    public class TableColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (TableStatus)value;
            switch (status)
            {
                case TableStatus.OCCUPIED:
                    return App.Current.Resources["OccupiedTable"];//vàng
                case TableStatus.CLEAN:
                    return App.Current.Resources["CleanTable"];//xanh
                default:
                    return App.Current.Resources["DirtyTable"];//đỏ
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
