using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Datas
{
    public static class Configuration
    {
        public static string Host = "http://192.168.137.1:8080";
        public static string Api(string endpoint)
        {
            return $"{Host}/{endpoint}";
        }
    }
}
