using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Restaurant.Converters
{
    public class IsManagerKitchenConverter : IValueConverter
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
            if (name == "Manager" || name == "Kitchen")
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
