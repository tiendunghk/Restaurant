using Restaurant.Mvvm.Command;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Restaurant.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using Syncfusion.XForms.EffectsView;
using Restaurant.Services.Navigation;
using System.Linq;
using Restaurant.Services;
using Restaurant.Datas;

namespace Restaurant.ViewModels.Order
{
    public class ListOrderViewModel : ViewModelBase
    {
        int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetProperty(ref _selectedIndex, value);
        }
        DelegateCommand<OrderModel> _itemTappedCommand;
        public DelegateCommand<OrderModel> ItemTappedCommand => _itemTappedCommand ??= new DelegateCommand<OrderModel>(Tapped);
        List<string> _filters;
        public List<string> Filters
        {
            get => _filters;
            set => SetProperty(ref _filters, value);
        }
        List<OrderModel> _listOrders;
        public List<OrderModel> ListOrders
        {
            get => _listOrders;
            set => SetProperty(ref _listOrders, value);
        }
        bool _isNoData;
        public bool IsNoData { get => _isNoData; set => SetProperty(ref _isNoData, value); }
        public List<OrderModel> ListOrdersBackup { get; set; } = new List<OrderModel>();
        public ListOrderViewModel()
        {
            IsNoData = true;

            Filters = new List<string> { "Tất cả", "Cần thanh toán", "Đã thanh toán" };
            SelectedIndex = 0;
            MessagingCenter.Subscribe<string>("abc", "LoadDataOrder", async (a) =>
            {
                IsLoadingData = true;
                var orders = await HttpService.GetAsync<List<OrderModel>>(Configuration.Api("order/getall"));
                orders = orders.Where(or => or.OrderDate?.Date == DateTime.Now.Date).ToList();
                foreach (var e in orders)
                {
                    e.TableName = Tables.ListTables.Find(x => x.Id == e.TableId).TableName;
                }
                ListOrders = orders;
                ListOrdersBackup = ListOrders;
                if (ListOrders.Count > 0) IsNoData = false;
                IsLoadingData = false;
            });
        }
        async void Tapped(OrderModel obj)
        {
            Dish d;
            List<OrderDetailUI> orderDetailUIs = new List<OrderDetailUI>();

            var orderdetails = await HttpService.GetAsync<List<OrderDetail>>(Configuration.Api($"orderdetail/byorder/{obj.Id}"));
            foreach (var e in orderdetails)
            {
                if (e.OrderDetail_OrderID == obj.Id)
                {
                    d = Datas.Dishs.ListDishs.ToList().Find(x => x.Id == e.DishId);
                    orderDetailUIs.Add(new OrderDetailUI
                    {
                        OrderDetail = e,
                        NameDish = d.Name,
                        ImageUrl = d.DishImage,
                        Status = e.OrderDetailStatus,
                        Price = d.Price,
                        Dish = d
                    });
                }
            }
            await NavigationService.NavigateToAsync<OrderDetailViewModel>(new NavigationParameters
            {
                {"listorderdetails",orderDetailUIs },
                {"tongtien",obj.OrderTotalAmount },
                {"orderstatus",obj.Status }
            });
        }
        public void PickerChanged()
        {
            switch (SelectedIndex)
            {
                case 0:
                    ListOrders = ListOrdersBackup;
                    break;
                case 1:
                    ListOrders = ListOrdersBackup.Where(x => x.Status == OrderStatus.PENDING).ToList();
                    break;
                case 2:
                    ListOrders = ListOrdersBackup.Where(x => x.Status == OrderStatus.COMPLETED).ToList();
                    break;
            }
        }
    }
}
