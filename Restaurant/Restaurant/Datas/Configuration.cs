using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Datas
{
    public static class Configuration
    {
        public static string Host = "https://restaurantnetcore.herokuapp.com";
        public static string Api(string endpoint)
        {
            return $"{Host}/{endpoint}";
        }
    }
}
