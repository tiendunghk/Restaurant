using Acr.UserDialogs;
using Restaurant.Datas;
using Restaurant.Models;
using Restaurant.Services;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels.Reports
{
    public class FoodReportViewModel : ViewModelBase
    {
        bool _orders;
        public bool Orders { get => _orders; set => SetProperty(ref _orders, value); }
        bool _revenue;
        public bool Revenue { get => _revenue; set => SetProperty(ref _revenue, value); }
        List<string> _pickers;
        public List<string> Pickers
        {
            get => _pickers;
            set => SetProperty(ref _pickers, value);
        }
        int _selectedIndex;
        public int SelectedIndex { get => _selectedIndex; set => SetProperty(ref _selectedIndex, value); }
        DateTime _date;
        public DateTime Date { get => _date; set => SetProperty(ref _date, value); }

        List<Dish> _listDishsRevenue;
        public List<Dish> ListDishsRevenue { get => _listDishsRevenue; set => SetProperty(ref _listDishsRevenue, value); }
        List<Dish> _listDishsOrder;
        public List<Dish> ListDishsOrder { get => _listDishsOrder; set => SetProperty(ref _listDishsOrder, value); }
        public FoodReportViewModel()
        {
            Pickers = new List<string>
            {
                "Số lượng order",
                "Doanh thu",
            };
            SelectedIndex = 0;
            Date = DateTime.Now;
            ListDishsRevenue = new List<Dish>();
            ListDishsOrder = new List<Dish>();
            Orders = true;
            Revenue = false;
        }
        public override async Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            ListDishsRevenue = await HttpService.GetAsync<List<Dish>>(Configuration.Api("dish/getall/true"));
            await FilterDatas();
        }
        public void ComboBoxChanged()
        {
            switch (SelectedIndex)
            {
                case 0:
                    Orders = true;
                    Revenue = false;
                    break;
                case 1:
                    Orders = false;
                    Revenue = true;
                    break;
            }
        }
        async Task FilterDatas()
        {
            using (UserDialogs.Instance.Loading("Wating..."))
            {
                Dictionary<Dish, decimal> mostRevenue = new Dictionary<Dish, decimal>();
                Dictionary<Dish, int> mostOrders = new Dictionary<Dish, int>();
                var foods = await HttpService.GetAsync<List<Dish>>(Configuration.Api($"dish/getall/false"));
                var orders = await HttpService.GetAsync<List<OrderModel>>(Configuration.Api("order/getall"));
                orders = orders.Where(x => x.OrderDate?.Date.Month == DateTime.Now.Month).ToList();
                foreach (var e in foods)
                {
                    decimal doanhthu = 0;
                    int count = 0;
                    foreach (var elem in orders)
                    {
                        var orderDetails = await HttpService.GetAsync<List<OrderDetail>>($"orderdetail/byorder/{elem.Id}");
                        doanhthu += orderDetails.Where(x => x.DishId == e.Id).Sum(x => e.Price);
                        count += orderDetails.Where(x => x.DishId == e.Id).Count();
                    }
                    mostRevenue.Add(e, doanhthu);
                    mostOrders.Add(e, count);
                }
                var revenue = mostRevenue.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                var morders = mostOrders.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                foreach (var p in revenue)
                {
                    p.Key.TotalAmount = p.Value;
                    ListDishsRevenue.Add(p.Key);
                }
                foreach (var p in morders)
                {
                    p.Key.SoLuong = p.Value;
                    ListDishsOrder.Add(p.Key);
                }
            }
        }
    }
}
