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

namespace Restaurant.ViewModels
{
    public class KitchenListFoodViewModel : ViewModelBase
    {
        DelegateCommand<OrderDetailUI> _callWaiterCommand;
        public DelegateCommand<OrderDetailUI> CallWaiterCommand => _callWaiterCommand ??= new DelegateCommand<OrderDetailUI>(CallWaiter);
        ObservableCollection<FoodHeaderInfo> _listItems;
        public ObservableCollection<FoodHeaderInfo> ListItems
        {
            get => _listItems;
            set => SetProperty(ref _listItems, value);
        }
        public KitchenListFoodViewModel()
        {
            FakeData();
            MessagingCenter.Subscribe<string>("abc", "LoadDataKitchen", async (a) =>
            {
                IsLoadingData = true;
                await Task.Delay(2000);
                IsLoadingData = false;
            });
            MessagingCenter.Subscribe<string,string>("a", "OnNotificationReceived", (a,b) =>
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
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            return base.OnNavigationAsync(parameters, navigationType);
        }
        List<OrderModel> _listOrders;
        public List<OrderModel> ListOrders
        {
            get => _listOrders;
            set => SetProperty(ref _listOrders, value);
        }
        void FakeData()
        {
            ListOrders = new List<OrderModel>();
            ListItems = new ObservableCollection<FoodHeaderInfo>();
            FoodHeaderInfo obj;

            var d = new Dish
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = "Món " + 99,
                Description = "Quá hấp dẫn",
                Price = 30000,
                DishImage = "com_tam.jpg",
            };

            for (int i = 0; i < 10; i++)
            {
                ListOrders.Add(new OrderModel
                {
                    Id = Guid.NewGuid().ToString("N"),
                    OrderDate = DateTime.Now,
                    OrderTotalAmount = 800000,
                    Status = i / 3 == 0 ? OrderStatus.COMPLETED : OrderStatus.PENDING,
                    TableName = "Bàn " + new Random().Next(20)
                });
            }

            for (int i = 0; i < 10; i++)//10 order
            {
                obj = new FoodHeaderInfo();
                obj.Header = ListOrders[i].TableName;
                for (int j = 0; j < 5; j++)//mỗi order 5 món
                {
                    obj.Add(new OrderDetailUI
                    {
                        OrderDetailUIId = Guid.NewGuid().ToString("N"),
                        OrderId = ListOrders[i].Id,
                        OrderDetail = new OrderDetail
                        {
                            OrderDetailId = Guid.NewGuid().ToString("N"),
                            DishId = d.Id
                        },
                        NameDish = d.Name,
                        ImageUrl = d.DishImage,
                        Status = j / 2 == 1 ? OrderDetailStatus.WAITING : OrderDetailStatus.COOKING,
                        Price = d.Price,
                        Dish = d,
                    });
                }
                ListItems.Add(obj);
            }
        }
    }
}