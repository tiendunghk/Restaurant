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
    public class OrderReportViewModel : ViewModelBase
    {
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
                "Tất cả",
                "Doanh thu cao nhất",
                "Doanh thu thấp nhất"
            };
            SelectedIndex = 0;
            Date = DateTime.Now;
        }
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            ListOrders = new List<OrderModel>();
            return base.OnNavigationAsync(parameters, navigationType);
        }
        public async void DateChanged()
        {
            var a = Date;
            await FilterOrders();
        }
        public async void ComboBoxChanged()
        {
            var b = SelectedIndex;
            await FilterOrders();
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
