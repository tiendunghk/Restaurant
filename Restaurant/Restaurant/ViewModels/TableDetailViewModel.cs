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
                "Đang nấu","Đã phục vụ","Đang chờ"
            };
            SelectedIndex = 0;
            Tests = new ObservableCollection<Dish>(Datas.Dishs.ListDishs);
            BackupDish = Tests;
            OrderedItems = new ObservableCollection<OrderDetailUI>();
        }
        Table _table;
        public Table Table { get => _table; set => SetProperty(ref _table, value); }
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            string v = "";
            parameters.TryGetValue("title", out v);
            parameters.TryGetValue("table", out var table);
            Table = table as Table;
            Title = v;
            if (App.Context.ListOrderDetailUI.TryGetValue(Table.Id, out var b))
            {
                OrderedItems = new ObservableCollection<OrderDetailUI>(b.ToList());
            }
            else
                OrderedItems = new ObservableCollection<OrderDetailUI>();
            return Task.CompletedTask;
        }
        void Tapped(object o)
        {

        }
        DelegateCommand _submitCommand;
        public DelegateCommand SubmitCommand => _submitCommand ??= new DelegateCommand(Submit, () => CanSubmit()).ObservesProperty(() => Tests);
        void Submit()
        {
            var a = Tests;
            if (order == null) order = new OrderModel { Id = Guid.NewGuid().ToString("N") };
            foreach (var elem in BackupDish)
            {
                if (elem.SoLuong > 0)
                {
                    for (int i = 0; i < elem.SoLuong; i++)
                    {
                        OrderedItems.Add(new OrderDetailUI
                        {
                            OrderDetailUIId = Guid.NewGuid().ToString("N"),
                            OrderDetail = new OrderDetail
                            {
                                OrderDetailId = Guid.NewGuid().ToString("N"),
                                DishId = elem.Id,
                                OrderDetail_OrderID = order.Id,
                            },
                            NameDish = elem.Name,
                            ImageUrl = elem.DishImage,
                            Status = OrderDetailStatus.WAITING,
                            Dish = elem,
                        });
                    }
                    elem.SoLuong = 0;
                }
            }
            if (!App.Context.ListOrderDetailUI.TryGetValue(Table.Id, out var x))
            {
                App.Context.ListOrderDetailUI.Add(Table.Id, OrderedItems);
            }
            else
                App.Context.ListOrderDetailUI[Table.Id] = OrderedItems;
            RaisePropertyChanged(nameof(OrderedItems));
        }
        bool CanSubmit()
        {
            var count = Tests.Where(x => x.SoLuong > 0).Count();
            return count > 0 ? true : false;
        }
        DelegateCommand _purchaseCommand;
        public DelegateCommand PurchaseCommand => _purchaseCommand ??= new DelegateCommand(Purchase, () => CanPurchase()).ObservesProperty(() => OrderedItems);
        bool CanPurchase()
        {
            if (OrderedItems.Count > 0 && OrderedItems != null) return true;
            return false;
        }
        void Purchase()
        {
            DialogService.ShowToast("Đã chuyển yêu cầu đến Cashier");
            OrderedItems = new ObservableCollection<OrderDetailUI>();
            App.Context.ListOrderDetailUI[Table.Id] = OrderedItems;
        }
        DelegateCommand<OrderDetailUI> _deleteDetailCommand;
        public DelegateCommand<OrderDetailUI> DeleteDetailCommand => _deleteDetailCommand ??= new DelegateCommand<OrderDetailUI>(DeleteDetail);
        void DeleteDetail(OrderDetailUI obj)
        {
            OrderedItems.Remove(obj);
            if (App.Context.ListOrderDetailUI.TryGetValue(Table.Id, out var x))
                App.Context.ListOrderDetailUI[Table.Id] = OrderedItems;
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
            if (Table.Status == TableStatus.OCCUPIED)
            {
                Table.Status = TableStatus.DIRTY;
                RaisePropertyChanged(nameof(Table));
                await DialogService.ShowAlertAsync("Trạng thái bàn đã chuyển từ OCCUPIED sang DIRTY", "Thông báo", "OK");
                return;
            }
            if (Table.Status == TableStatus.DIRTY)
            {
                Table.Status = TableStatus.CLEAN;
                RaisePropertyChanged(nameof(Table));
                await DialogService.ShowAlertAsync("Trạng thái bàn đã chuyển từ DIRTY sang CLEAN", "Thông báo", "OK");
                return;
            }
            if (Table.Status == TableStatus.CLEAN)
            {
                Table.Status = TableStatus.OCCUPIED;
                RaisePropertyChanged(nameof(Table));
                await DialogService.ShowAlertAsync("Trạng thái bàn đã chuyển từ CLEAN sang OCCUPIED", "Thông báo", "OK");
                return;
            }
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
    }
}
