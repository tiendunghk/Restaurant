using Acr.UserDialogs;
using Restaurant.Datas;
using Restaurant.Models;
using Restaurant.Services;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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


        public ObservableCollection<Dish> ListOrderes = new ObservableCollection<Dish>();
        public ObservableCollection<Dish> ListRevenues = new ObservableCollection<Dish>();
        public FoodReportViewModel()
        {
            Pickers = new List<string>
            {
                "Số lượng order",
                "Doanh thu",
            };
            SelectedIndex = 0;
            Date = DateTime.Now;
            Orders = true;
            Revenue = false;
        }
        public override async Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            await FilterDatas();
            //ListDishsOrder = await HttpService.GetAsync<List<Dish>>(Configuration.Api($"dish/getall/false"));
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
        async Task<List<OrderDetail>> GetOrderDetails(string orderId, string dishId)
        {
            var orderDetails = await HttpService.GetAsync<List<OrderDetail>>(Configuration.Api($"orderdetail/byorder/{orderId}"));
            orderDetails = orderDetails.Where(x => x.DishId == dishId).ToList();
            return orderDetails;
        }
        async Task FilterDatas()
        {
            using (UserDialogs.Instance.Loading("Wating..."))
            {
                Dictionary<Dish, decimal> mostRevenue = new Dictionary<Dish, decimal>();
                Dictionary<Dish, int> mostOrders = new Dictionary<Dish, int>();
                var foods = await HttpService.GetAsync<List<Dish>>(Configuration.Api($"dish/getall/false"));
                var orders = await HttpService.GetAsync<List<OrderModel>>(Configuration.Api("order/getall"));
                orders = orders.Where(x => x.OrderDate?.ToLocalTime().Month == DateTime.Now.Month).ToList();
                foreach (var e in foods)
                {
                    decimal doanhthu = 0;
                    int count = 0;
                    var tasks = new List<Task<List<OrderDetail>>>();
                    for (int i = 0; i < orders.Count; i++)
                    {
                        tasks.Add(GetOrderDetails(orders[i].Id, e.Id));
                    }
                    foreach (var task in await Task.WhenAll(tasks))
                    {
                        doanhthu += task.Sum(x => e.Price);
                        count += task.Where(x => x.DishId == e.Id).Count();
                    }
                    mostRevenue.Add(e, doanhthu);
                    mostOrders.Add(e, count);
                }
                var c = mostOrders.Count();
                var cc = mostOrders.Count();
                var revenue = mostRevenue.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                var morders = mostOrders.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                var _LRevenues = new List<Dish>();
                var _LOrders = new List<Dish>();
                foreach (var p in revenue)
                {
                    p.Key.TotalAmount = p.Value;
                    _LRevenues.Add(p.Key);
                }
                foreach (var p in morders)
                {
                    p.Key.SoLuong = p.Value;
                    _LOrders.Add(p.Key);
                }
                ListDishsOrder = _LOrders.OrderByDescending(x => x.SoLuong).ToList();
                ListDishsRevenue = _LRevenues.OrderByDescending(x => x.TotalAmount).ToList();
            }
        }
    }
}
