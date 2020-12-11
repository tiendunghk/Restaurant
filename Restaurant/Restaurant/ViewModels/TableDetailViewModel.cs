using Restaurant.Models;
using Restaurant.Mvvm.Command;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Restaurant.Services;
using Restaurant.Datas;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Restaurant.Services.Notification;
using Acr.UserDialogs;

namespace Restaurant.ViewModels
{
    public class TableDetailViewModel : ViewModelBase
    {
        bool _isChecked;
        DelegateCommand _changeTableStatusCommand;
        OrderModel order = null;
        DelegateCommand _searchCommand;
        DelegateCommand<object> _tappedCommand;
        DelegateCommand<Dish> _detailCommand;
        DelegateCommand<OrderDetailUI> _viewDishCommand;
        public DelegateCommand<object> TappedCommand => _tappedCommand ??= new DelegateCommand<object>(Tapped);
        public DelegateCommand<Dish> DetailCommand => _detailCommand ??= new DelegateCommand<Dish>(ShowDetail);
        public DelegateCommand SearchCommand => _searchCommand ??= new DelegateCommand(Search);
        public DelegateCommand ChangeTableStatusCommand => _changeTableStatusCommand ??= new DelegateCommand(ChangeTableStatus);
        public DelegateCommand<OrderDetailUI> ViewDishCommand => _viewDishCommand ??= new DelegateCommand<OrderDetailUI>(ViewDish);

        ObservableCollection<Dish> _tests;
        public ObservableCollection<Dish> BackupDish { get; set; }
        string _monan;
        public string MonAn
        {
            get => _monan;
            set
            {
                SetProperty(ref _monan, value);
                if (string.IsNullOrEmpty(value))
                    Tests = BackupDish;
            }
        }
        bool _searchMonAn;
        public bool SearchMonAn
        {
            get => _searchMonAn;
            set => SetProperty(ref _searchMonAn, value);
        }
        public ObservableCollection<Dish> Tests
        {
            get => _tests;
            set => SetProperty(ref _tests, value);
        }


        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }


        ObservableCollection<OrderDetailUI> _orderedItems;
        public ObservableCollection<OrderDetailUI> OrderedItems
        {
            get => _orderedItems;
            set => SetProperty(ref _orderedItems, value);
        }
        public ObservableCollection<OrderDetailUI> OrderedItemsBackup { get; set; } = new ObservableCollection<OrderDetailUI>();

        List<string> _filters;
        public List<string> Filters
        {
            get => _filters;
            set => SetProperty(ref _filters, value);
        }
        int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetProperty(ref _selectedIndex, value);
        }
        public TableDetailViewModel()
        {
            Filters = new List<string>
            {
                "Tất cả","Đang nấu","Đã phục vụ","Đang chờ"
            };
            SelectedIndex = 0;
            Tests = new ObservableCollection<Dish>(Datas.Dishs.ListDishs);
            BackupDish = Tests;
            OrderedItems = new ObservableCollection<OrderDetailUI>();
        }
        Table _table;
        public Table Table { get => _table; set => SetProperty(ref _table, value); }
        public override async Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            string v = "";
            parameters.TryGetValue("title", out v);
            parameters.TryGetValue("table", out var table);
            Table = table as Table;
            if (!string.IsNullOrEmpty(Table.TableIdOrderServing))
            {
                order = await HttpService.GetAsync<OrderModel>(Configuration.Api($"order/{Table.TableIdOrderServing}"));
                if (App.Context.CurrentOrder.ContainsKey(Table.Id))
                {
                    App.Context.CurrentOrder[Table.Id] = order;
                }
                else
                {
                    App.Context.CurrentOrder.Add(Table.Id, order);
                }
            }
            Title = v;
        }
        void Tapped(object o)
        {

        }
        DelegateCommand _submitCommand;
        public DelegateCommand SubmitCommand => _submitCommand ??= new DelegateCommand(Submit);
        async void Submit()
        {
            var b = BackupDish.Where(x => x.SoLuong > 0).Count() > 0 ? true : false;
            if (!b)
            {
                await DialogService.ShowAlertAsync("Vui lòng kiểm tra lại số lượng", "Thông báo", "OK");
                return;
            }
            if (order != null && order.Status == OrderStatus.REQUESTPAYMENT)
            {
                await DialogService.ShowAlertAsync("Không thể order thêm, đang chờ thanh toán", "Thông báo", "OK");
                return;
            }
            if (order == null) order = new OrderModel { OrderDate = DateTime.Now, TableId = Table.Id, StaffId = App.Context.CurrentStaff.Id, TableName = Table.TableName };

            decimal cost = 0;
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var elem in BackupDish)
            {
                if (elem.SoLuong > 0)
                {
                    for (int i = 0; i < elem.SoLuong; i++)
                    {
                        var OrderDetail = new OrderDetail
                        {
                            DishId = elem.Id,
                            OrderDetailStatus = OrderDetailStatus.WAITING,
                        };
                        cost += Datas.Dishs.ListDishs.ToList().Find(x => x.Id == elem.Id).Price;
                        orderDetails.Add(OrderDetail);
                    }
                    elem.SoLuong = 0;
                }
            }
            order.OrderTotalAmount += cost;

            //chưa có order nào với bàn này
            if (!App.Context.CurrentOrder.TryGetValue(Table.Id, out var op) || !App.Context.CurrentOrder.ContainsKey(Table.Id))
            {
                var output = await HttpService.PostApiAsync<JObject>(Configuration.Api("order/add"), order);
                order.Id = output["id"].ToString();
                Table.TableIdOrderServing = order.Id;
                if (App.Context.CurrentOrder.ContainsKey(Table.Id))
                    App.Context.CurrentOrder[Table.Id] = order;
                else
                    App.Context.CurrentOrder.Add(Table.Id, order);
            }
            //đã có order với bàn
            else
            {
                await HttpService.PostApiAsync<object>(Configuration.Api("order/update"), order);
                App.Context.CurrentOrder[Table.Id] = order;
            }

            await HttpService.PostApiAsync<object>(Configuration.Api("table/update"), Table);
            foreach (var e in orderDetails)
            {
                e.OrderDetail_OrderID = order.Id;
                await HttpService.PostApiAsync<object>(Configuration.Api("orderdetail/add"), e);
            }

            OrderedItemsBackup = OrderedItems;
            PickerChanged();
            RaisePropertyChanged(nameof(OrderedItems));
        }
        DelegateCommand _purchaseCommand;
        public DelegateCommand PurchaseCommand => _purchaseCommand ??= new DelegateCommand(Purchase);
        async void Purchase()
        {
            if (OrderedItems.Count < 1)
            {
                await DialogService.ShowAlertAsync("Vui lòng kiểm tra lại số lượng", "Thông báo", "OK");
                return;
            }
            if (order.Status == OrderStatus.REQUESTPAYMENT || order.Status == OrderStatus.COMPLETED) return;
            using (UserDialogs.Instance.Loading("Waiting..."))
            {
                order.Status = OrderStatus.REQUESTPAYMENT;
                await HttpService.PostApiAsync<object>(Configuration.Api("order/update"), order);
                await PushNotiCashier();
                DialogService.ShowToast("Đã chuyển yêu cầu đến Cashier");
                OrderedItems = OrderedItemsBackup = new ObservableCollection<OrderDetailUI>();
                //Table.TableIdOrderServing = null;
                App.Context.CurrentOrder.Remove(Table.Id);
                //await HttpService.PostApiAsync<object>(Configuration.Api("table/update"), Table);
                await NavigationService.NavigateBackAsync();
            }
        }
        DelegateCommand<OrderDetailUI> _deleteDetailCommand;
        public DelegateCommand<OrderDetailUI> DeleteDetailCommand => _deleteDetailCommand ??= new DelegateCommand<OrderDetailUI>(DeleteDetail);
        async void DeleteDetail(OrderDetailUI obj)
        {
            OrderedItems.Remove(obj);
            OrderedItemsBackup.Remove(obj);
            var idOrderDetail = obj.OrderDetail.OrderDetailId;
            order.OrderTotalAmount -= obj.Dish.Price;
            await HttpService.PostApiAsync<object>(Configuration.Api($"orderdetail/{idOrderDetail}"), new { });
            await HttpService.PostApiAsync<object>(Configuration.Api($"order/update"), order);
            RaisePropertyChanged(nameof(OrderedItems));
            DialogService.ShowToast("Đã xóa item");
        }
        async void ShowDetail(Dish dish)
        {
            await NavigationService.NavigateToAsync<DishDetailViewModel>(new NavigationParameters
            {
                {"Food",dish }
            });
        }
        async void Search()
        {
            SearchMonAn = true;
            var unicodeKeyword = Helpers.RemoveSign4VietnameseString(MonAn).ToLower();
            Tests = new ObservableCollection<Dish>();
            await Task.Delay(1500);
            SearchMonAn = false;
            Tests = new ObservableCollection<Dish>(BackupDish.Where(x => Helpers.RemoveSign4VietnameseString(x.Name).ToLower().Contains(unicodeKeyword)));
        }
        async void ChangeTableStatus()
        {
            int count = 0;
            if (Table.Status == TableStatus.OCCUPIED && count < 1)
            {
                count++;
                Table.Status = TableStatus.DIRTY;
                RaisePropertyChanged(nameof(Table));
                //await DialogService.ShowAlertAsync("Trạng thái bàn đã chuyển từ OCCUPIED sang DIRTY", "Thông báo", "OK");
                //return;
            }
            if (Table.Status == TableStatus.DIRTY && count < 1)
            {
                count++;
                Table.Status = TableStatus.CLEAN;
                RaisePropertyChanged(nameof(Table));
                //await DialogService.ShowAlertAsync("Trạng thái bàn đã chuyển từ DIRTY sang CLEAN", "Thông báo", "OK");
                //return;
            }
            if (Table.Status == TableStatus.CLEAN && count < 1)
            {
                count++;
                Table.Status = TableStatus.OCCUPIED;
                RaisePropertyChanged(nameof(Table));
                //await DialogService.ShowAlertAsync("Trạng thái bàn đã chuyển từ CLEAN sang OCCUPIED", "Thông báo", "OK");
                //return;
            }
            var json = JsonConvert.SerializeObject(Table);
            await HttpService.PostApiAsync<object>(Configuration.Api("table/update"), Table);
            //await NavigationService.NavigateBackAsync();
        }
        async void ViewDish(OrderDetailUI ui)
        {
            await NavigationService.NavigateToAsync<DishDetailViewModel>(new NavigationParameters
            {
                {"Food",ui.Dish }
            });
        }
        public void StepperValue()
        {
            RaisePropertyChanged(nameof(Tests));
        }
        public void PickerChanged()
        {
            if (OrderedItemsBackup.Count > 0)
            {
                switch (SelectedIndex)
                {
                    case 0:
                        OrderedItems = OrderedItemsBackup;
                        break;
                    case 1:
                        OrderedItems = new ObservableCollection<OrderDetailUI>(OrderedItemsBackup.Where(x => x.Status == OrderDetailStatus.COOKING));
                        break;
                    case 2:
                        OrderedItems = new ObservableCollection<OrderDetailUI>(OrderedItemsBackup.Where(x => x.Status == OrderDetailStatus.COMPLETED));
                        break;
                    case 3:
                        OrderedItems = new ObservableCollection<OrderDetailUI>(OrderedItemsBackup.Where(x => x.Status == OrderDetailStatus.WAITING));
                        break;
                }
            }
        }
        public async Task LoadOrderDetailData()
        {
            if (string.IsNullOrEmpty(Table.TableIdOrderServing)) return;
            OrderedItems = new ObservableCollection<OrderDetailUI>();
            var orderdetails = await HttpService.GetAsync<List<OrderDetail>>(Configuration.Api($"orderdetail/byorder/{Table.TableIdOrderServing}"));
            foreach (var e in orderdetails)
            {
                OrderedItems.Add(new OrderDetailUI
                {
                    OrderDetailUIId = Guid.NewGuid().ToString("N"),
                    OrderDetail = e,
                    NameDish = Dishs.ListDishs.ToList().Find(x => x.Id == e.DishId).Name,
                    ImageUrl = Dishs.ListDishs.ToList().Find(x => x.Id == e.DishId).DishImage,
                    Status = e.OrderDetailStatus,
                    Dish = Dishs.ListDishs.ToList().Find(x => x.Id == e.DishId),
                });
            }
            OrderedItemsBackup = OrderedItems;
            //RaisePropertyChanged(nameof(OrderedItemsBackup));
        }
        async Task PushNotiCashier()
        {
            var listStaffs = await HttpService.GetAsync<List<Staff>>(Configuration.Api("staff/getall/true"));
            listStaffs = listStaffs.Where(x => x.Role == "5").ToList();
            var externalIds = listStaffs.Select(x => x.ExternalId).ToList();
            Notification.PushExternalID(new { flag = NotiFlag.SENDTOCASHIER }, externalIds, "Có yêu cầu thanh toán mới.");
        }
    }
}
