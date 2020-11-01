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
        public ListOrderViewModel()
        {

            Filters = new List<string> { "Hôm nay", "Cần thanh toán", "Chưa thanh toán" };
            SelectedIndex = 0;

            ListOrders = new List<OrderModel>();
            for (int i = 0; i < 10; i++)
            {
                ListOrders.Add(new OrderModel
                {
                    Id = Guid.NewGuid().ToString("N"),
                    OrderDate = DateTime.Now,
                    OrderTotalAmount = 800000,
                    Status = i / 3 == 0 ? OrderStatus.COMPLETED : OrderStatus.PENDING
                });
            }
            MessagingCenter.Subscribe<string>("abc", "LoadDataOrder", async (a) =>
            {
                IsLoadingData = true;
                await Task.Delay(2000);
                IsLoadingData = false;
            });
        }
        async void Tapped(OrderModel obj)
        {
            List<OrderDetailUI> orderDetailUIs = new List<OrderDetailUI>();
            var d = new Dish
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = "Món " + 99,
                Description = "Quá hấp dẫn",
                Price = 30000,
                DishImage = "com_tam.jpg",
            };
            for (int i = 0; i < 5; i++)
            {
                orderDetailUIs.Add(new OrderDetailUI
                {
                    OrderDetail = new OrderDetail
                    {
                        OrderDetailId = Guid.NewGuid().ToString("N"),
                        DishId = d.Id
                    },
                    NameDish = d.Name,
                    ImageUrl = d.DishImage,
                    Status = OrderDetailStatus.WAITING,
                    Price = d.Price,
                    Dish=d
                });
            }
            await NavigationService.NavigateToAsync<OrderDetailViewModel>(new NavigationParameters
            {
                { "listorderdetails",orderDetailUIs },
                {"tongtien",obj.OrderTotalAmount },
                {"orderstatus",obj.Status }
            });
        }
    }
}
