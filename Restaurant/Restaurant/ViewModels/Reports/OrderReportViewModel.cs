using Microcharts;
using Restaurant.Datas;
using Restaurant.Models;
using Restaurant.Services;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels.Reports
{
    public class OrderReportViewModel : ViewModelBase
    {
        bool _isVisible0;
        public bool IsVisible0 { get => _isVisible0; set => SetProperty(ref _isVisible0, value); }
        bool _isVisible1;
        public bool IsVisible1 { get => _isVisible1; set => SetProperty(ref _isVisible1, value); }
        BarChart _barChartOrders;
        public BarChart BarChartOrders
        {
            get => _barChartOrders;
            set => SetProperty(ref _barChartOrders, value);
        }
        BarChart _barChartRevenues;
        public BarChart BarChartRevenues
        {
            get => _barChartRevenues;
            set => SetProperty(ref _barChartRevenues, value);
        }
        List<string> _pickers;
        public List<string> Pickers
        {
            get => _pickers;
            set => SetProperty(ref _pickers, value);
        }
        int _selectedIndex;
        public int SelectedIndex { get => _selectedIndex; set => SetProperty(ref _selectedIndex, value); }
        List<OrderModel> _listOrders;
        public List<OrderModel> ListOrders { get => _listOrders; set => SetProperty(ref _listOrders, value); }
        public List<OrderModel> BackupModels = new List<OrderModel>();
        DateTime _date;
        public DateTime Date { get => _date; set => SetProperty(ref _date, value); }
        public OrderReportViewModel()
        {
            Pickers = new List<string>
            {
                //"Tất cả",
                "Doanh thu",
                "Số lượng order"
            };
            SelectedIndex = 0;
            Date = DateTime.Now;
            IsVisible0 = true;
            IsVisible1 = false;
        }
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            ListOrders = new List<OrderModel>();
            GetDatas();
            return base.OnNavigationAsync(parameters, navigationType);
        }
        async void GetDatas()
        {
            var blueColor = SKColor.Parse("#09C");
            var chartEntries = new List<ChartEntry>();
            var chartEntriesRevenue = new List<ChartEntry>();
            var listOrders = await HttpService.GetAsync<List<OrderModel>>(Configuration.Api("order/getall"));
            decimal doanhthu = 0;
            //số order hàng tháng
            for (int i = 1; i <= 12; i++)
            {
                var orderm = listOrders.Where(x => x.OrderDate?.Date.Month == i && x.OrderDate?.Date.Year == DateTime.Now.Year).Count();
                chartEntries.Add(new ChartEntry(orderm)
                {
                    Label = $"{i}",
                    ValueLabel = $"{orderm}",
                    Color = blueColor
                });

                doanhthu = listOrders.Where(x => x.OrderDate?.Date.Month == i && x.OrderDate?.Date.Year == DateTime.Now.Year).Sum(x => x.OrderTotalAmount);
                chartEntriesRevenue.Add(new ChartEntry((float)doanhthu)
                {
                    Label = $"{i}",
                    ValueLabel = $"{doanhthu}",
                    Color = blueColor
                });
            }

            BarChartOrders = new BarChart { Entries = chartEntries, LabelTextSize = 30f, LabelOrientation = Orientation.Horizontal };
            BarChartRevenues = new BarChart { Entries = chartEntriesRevenue, LabelTextSize = 30f, LabelOrientation = Orientation.Horizontal };
        }
        public async void DateChanged()
        {
            var a = Date;
            await FilterOrders();
        }
        public void ComboBoxChanged()
        {
            //var b = SelectedIndex;
            //await FilterOrders();
            switch (SelectedIndex)
            {
                case 0:
                    IsVisible1 = true;
                    IsVisible0 = false;
                    break;
                case 1:
                    IsVisible0 = true;
                    IsVisible1 = false;
                    break;
            }
        }
        async Task FilterOrders()
        {
            var listOrders = await HttpService.GetAsync<List<OrderModel>>(Configuration.Api("order/getall"));
            listOrders = listOrders.Where(x => x.OrderDate?.Date == Date.Date).ToList();
            foreach (var e in listOrders)
            {
                foreach (var elem in Tables.ListTables)
                {
                    if (e.TableId == elem.Id)
                    {
                        e.TableName = elem.TableName;
                        break;
                    }
                }
            }
            ListOrders = listOrders;
            switch (SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    ListOrders = ListOrders.OrderBy(x => x.OrderTotalAmount).Reverse().ToList();
                    break;
                case 2:
                    ListOrders = ListOrders.OrderBy(x => x.OrderTotalAmount).ToList();
                    break;
                default:
                    break;
            }
        }
    }
}
