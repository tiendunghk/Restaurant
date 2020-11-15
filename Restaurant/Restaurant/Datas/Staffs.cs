using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Datas
{
    public static class Staffs
    {
        public static List<Staff> ListStaffs = new List<Staff>
        {
            new Staff{Id="nv1",Name="Nam",Role="Waiter",UserName="a",PassWord="1"},
            new Staff{Id="nv2",Name="Bao",Role="Manager",UserName="b",PassWord="1"},
            new Staff{Id="nv3",Name="An",Role="Busboy",UserName="c",PassWord="1"},
            new Staff{Id="nv4",Name="Dinh",Role="Cashier",UserName="d",PassWord="1"},
            new Staff{Id="nv5",Name="Dung",Role="Kitchen",UserName="e",PassWord="1"},
        };
    }
}
