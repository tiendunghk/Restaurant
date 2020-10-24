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
            var status = (int)value;
            switch (status)
            {
                case 0:
                    return App.Current.Resources["OccupiedTable"];
                case 1:
                    return App.Current.Resources["CleanTable"];
                default:
                    return App.Current.Resources["DirtyTable"];
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
