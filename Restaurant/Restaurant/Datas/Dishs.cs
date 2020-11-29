using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Restaurant.Datas
{
    public static class Dishs
    {
        public static ObservableCollection<Dish> ListDishs;
        public static List<Dish> ListDishs1 = new List<Dish>
        {
            new Dish{Id="d1",Name="Cơm gà",DishImage="com_tam",Description="Cơm gà hấp dẫn nè",Price=30000,IsActive=true},
            new Dish{Id="d2",Name="Cơm chiên",DishImage="com_tam",Description="Cơm chiên dương châu số 1",Price=28000,IsActive=true},
            new Dish{Id="d3",Name="Lẩu thái",DishImage="com_tam",Description="Thailand",Price=100000,IsActive=true},
            new Dish{Id="d4",Name="Mỳ Ý",DishImage="com_tam",Description="Xuất sắc",Price=90000,IsActive=false},
            new Dish{Id="d5",Name="Mỳ xào",DishImage="com_tam",Description="Nhào dzô",Price=30000,IsActive=true},
            new Dish{Id="d6",Name="Cơm trộn",DishImage="com_tam",Description="Mua đi nào",Price=55000,IsActive=true},
            new Dish{Id="d7",Name="Phở gà",DishImage="com_tam",Description="Tuyệt vời",Price=40000,IsActive=true},
            new Dish{Id="d8",Name="Bánh mì pate",DishImage="com_tam",Description="Quá ngon quá rẻ hahah",Price=15000,IsActive=true},
        };
    }
}
