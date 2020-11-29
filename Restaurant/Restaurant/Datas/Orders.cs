using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Datas
{
    public static class Orders
    {
        public static List<OrderModel> ListOrders = new List<OrderModel>
        {
            new OrderModel{Id="order1",TableId=Tables.ListTables[0].Id,OrderDate=DateTime.Now,OrderTotalAmount=90000,TableName="Bàn 1",Status=OrderStatus.COMPLETED},
            new OrderModel{Id="order2",TableId=Tables.ListTables[1].Id,OrderDate=DateTime.Now,OrderTotalAmount=90000,TableName="Bàn 2",Status=OrderStatus.COMPLETED},
            new OrderModel{Id="order3",TableId=Tables.ListTables[5].Id,OrderDate=DateTime.Now,OrderTotalAmount=90000,TableName="Bàn 6",Status=OrderStatus.PENDING},
        };
        public static List<OrderDetail> ListOrderDetails = new List<OrderDetail>
        {
            //order 1
            new OrderDetail{
                OrderDetailId="odt1",
                DishId=Dishs.ListDishs[0].Id,
                OrderDetail_OrderID="order1",
                OrderDetailStatus=OrderDetailStatus.WAITING
            },
            new OrderDetail{
                OrderDetailId="odt2",
                DishId=Dishs.ListDishs[0].Id,
                OrderDetail_OrderID="order1",
                OrderDetailStatus=OrderDetailStatus.WAITING
            },
            new OrderDetail{
                OrderDetailId="odt3",
                DishId=Dishs.ListDishs[3].Id,
                OrderDetail_OrderID="order1",
                OrderDetailStatus=OrderDetailStatus.COMPLETED
            },
            //order 2
            new OrderDetail{
                OrderDetailId="odt4",
                DishId=Dishs.ListDishs[3].Id,
                OrderDetail_OrderID="order2",
                OrderDetailStatus=OrderDetailStatus.WAITING
            },
            new OrderDetail{
                OrderDetailId="odt5",
                DishId=Dishs.ListDishs[6].Id,
                OrderDetail_OrderID="order2",
                OrderDetailStatus=OrderDetailStatus.WAITING
            },
            //order 3
            new OrderDetail{
                OrderDetailId="odt6",
                DishId=Dishs.ListDishs[1].Id,
                OrderDetail_OrderID="order3",
                OrderDetailStatus=OrderDetailStatus.COOKING
            },
            new OrderDetail{
                OrderDetailId="odt7",
                DishId=Dishs.ListDishs[1].Id,
                OrderDetail_OrderID="order3",
                OrderDetailStatus=OrderDetailStatus.WAITING
            },
            new OrderDetail{
                OrderDetailId="odt8",
                DishId=Dishs.ListDishs[7].Id,
                OrderDetail_OrderID="order3",
                OrderDetailStatus=OrderDetailStatus.COMPLETED
            },
        };
    }
}
