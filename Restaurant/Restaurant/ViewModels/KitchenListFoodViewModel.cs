using Restaurant.Datas;
using Restaurant.Models;
using Restaurant.Mvvm.Command;
using Restaurant.Services;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Restaurant.ViewModels
{
    public class KitchenListFoodViewModel : ViewModelBase
    {
        string _foodSearch;
        public string FoodSearch
        {
            get => _foodSearch;
            set
            {
                SetProperty(ref _foodSearch, value);
                if (string.IsNullOrEmpty(value))
                    ListItems = ListItemsBackup;
            }
        }
        DelegateCommand _searchCommand;
        DelegateCommand<OrderDetailUI> _callWaiterCommand;
        public DelegateCommand<OrderDetailUI> CallWaiterCommand => _callWaiterCommand ??= new DelegateCommand<OrderDetailUI>(CallWaiter);
        public DelegateCommand SearchCommand => _searchCommand ??= new DelegateCommand(Search);
        ObservableCollection<FoodHeaderInfo> _listItems;
        public ObservableCollection<FoodHeaderInfo> ListItems
        {
            get => _listItems;
            set => SetProperty(ref _listItems, value);
        }
        public ObservableCollection<FoodHeaderInfo> ListItemsBackup;
        bool _isNoData;
        public bool IsNoData { get => _isNoData; set => SetProperty(ref _isNoData, value); }
        public KitchenListFoodViewModel()
        {
            IsNoData = true;
            Title = "Danh sách đang chờ";
            MessagingCenter.Subscribe<string>("abc", "LoadDataKitchen", async (a) =>
            {
                IsLoadingData = true;
                await LoadData();
                if (ListOrders.Count > 0) IsNoData = false;
                IsLoadingData = false;
            });
            MessagingCenter.Subscribe<string, string>("a", "OnNtificationReceived", (a, b) =>
             {
                 Title = b;
             });
        }
        void CallWaiter(OrderDetailUI str)
        {
            if (str.Status != OrderDetailStatus.WAITING)
            {
                foreach (var elem in ListItems)
                {
                    foreach (var e in elem)
                    {
                        if (e.OrderDetailUIId == str.OrderDetailUIId)
                        {
                            Datas.Orders.ListOrderDetails.Remove(e.OrderDetail);
                            elem.Remove(e);
                            if (elem.Count == 0)
                                ListItems.Remove(elem);
                            DialogService.ShowToast("Đã thông báo cho waiter!");
                            return;
                        }
                    }
                }
            }
            else
            {
                str.Status = OrderDetailStatus.COOKING;
            }

        }
        public override async Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            IsLoadingData = true;
            await LoadData();
            IsLoadingData = false;
        }
        List<OrderModel> _listOrders;
        public List<OrderModel> ListOrders
        {
            get => _listOrders;
            set => SetProperty(ref _listOrders, value);
        }
        async Task LoadData()
        {
            var orders = await HttpService.GetAsync<List<OrderModel>>(Configuration.Api("order/getall"));
            orders = orders.Where(or => or.OrderDate?.Date == DateTime.Now.Date).ToList();
            foreach (var e in orders)
            {
                e.TableName = Tables.ListTables.Find(x => x.Id == e.TableId).TableName;
            }
            ListOrders = orders;
            ListItems = new ObservableCollection<FoodHeaderInfo>();
            FoodHeaderInfo obj;
            Dish d;

            for (int i = 0; i < ListOrders.Count; i++)
            {
                obj = new FoodHeaderInfo();
                obj.Header = ListOrders[i].TableName;
                var orderdetails = await HttpService.GetAsync<List<OrderDetail>>(Configuration.Api($"orderdetail/byorder/{ListOrders[i].Id}"));
                foreach (var e in orderdetails)
                {
                    if (e.OrderDetail_OrderID == ListOrders[i].Id)
                    {
                        d = Datas.Dishs.ListDishs.ToList().Find(x => x.Id == e.DishId);
                        obj.Add(new OrderDetailUI
                        {
                            OrderDetailUIId = Guid.NewGuid().ToString("N"),
                            OrderId = ListOrders[i].Id,
                            OrderDetail = e,
                            NameDish = d.Name,
                            ImageUrl = d.DishImage,
                            Status = e.OrderDetailStatus,
                            Price = d.Price,
                            Dish = d,
                        });
                    }
                }
                ListItems.Add(obj);
            }
            ListItemsBackup = ListItems;
        }
        async void Search()
        {
            FoodHeaderInfo obj;
            var unicodeKeyword = Helpers.RemoveSign4VietnameseString(FoodSearch).ToLower();
            var lists = ListItemsBackup;
            var pivot = new ObservableCollection<FoodHeaderInfo>();
            for (int i = 0; i < lists.Count; i++)
            {
                obj = new FoodHeaderInfo();
                for (int j = 0; j < lists[i].Count; j++)
                {
                    if (Helpers.RemoveSign4VietnameseString(lists[i][j].Dish.Name).ToLower().Contains(unicodeKeyword))
                    {
                        obj.Add(lists[i][j]);
                    }
                }
                if (obj.Count != 0)
                {
                    obj.Header = lists[i].Header;
                    pivot.Add(obj);
                }
            }
            ListItems = pivot;
        }
    }
}