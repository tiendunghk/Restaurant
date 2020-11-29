using Restaurant.Models;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
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
        public OrderReportViewModel()
        {
            Pickers = new List<string>
            {
                "Tất cả",
                "Doanh thu cao nhất",
                "Doanh thu thấp nhất"
            };
            SelectedIndex = 0;
        }
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            ListOrders = Datas.Orders.ListOrders;
            return base.OnNavigationAsync(parameters, navigationType);
        }
    }
}
