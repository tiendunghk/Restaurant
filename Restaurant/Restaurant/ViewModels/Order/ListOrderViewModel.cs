using Restaurant.Mvvm.Command;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Restaurant.Models;
namespace Restaurant.ViewModels.Order
{
    public class ListOrderViewModel : ViewModelBase
    {
        DelegateCommand<object> _itemTappedCommand;
        public DelegateCommand<object> ItemTappedCommand => _itemTappedCommand ??= new DelegateCommand<object>(Tapped);
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
            Filters = new List<string>();
            Filters.Add("Hôm nay");
            Filters.Add("Cần thanh toán");
            Filters.Add("Chưa thanh toán");

            ListOrders = new List<OrderModel>();
            for (int i = 0; i < 10; i++)
            {
                ListOrders.Add(new OrderModel
                {
                    Id = Guid.NewGuid().ToString("N"),
                    OrderDate = DateTime.Now,
                    OrderTotalAmount = 800000,
                });
            }
        }
        async void Tapped(object obj)
        {
            await NavigationService.NavigateToAsync<OrderDetailViewModel>();
        }
    }
}
