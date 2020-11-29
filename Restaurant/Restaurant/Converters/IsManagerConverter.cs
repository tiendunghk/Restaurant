using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Restaurant.Converters
{
    public class IsManagerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string name = string.Empty;
            var idRole = value.ToString();
            foreach (var e in Datas.Roles.ListRoles)
            {
                if (e.RoleId == idRole)
                    name = e.RoleName;
            }
            var b = name == "Manager" ? true : false;
            return b;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
