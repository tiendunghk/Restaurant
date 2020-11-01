using Restaurant.Models;
using Restaurant.Mvvm.Command;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels.Order
{
    public class OrderDetailViewModel : ViewModelBase
    {
        int _orderStatus = 0;
        public int OrderStatus
        {
            get => _orderStatus;
            set => SetProperty(ref _orderStatus, value);
        }
        DelegateCommand _purchaseCommand;
        public DelegateCommand PurchaseCommand => _purchaseCommand ??= new DelegateCommand(Purchase, () =>
        OrderStatus >= 1).ObservesProperty(() => OrderStatus);
        public OrderDetailViewModel()
        {

        }
        DelegateCommand<OrderDetailUI> _tapped;
        public DelegateCommand<OrderDetailUI> TappedCommand => _tapped ??= new DelegateCommand<OrderDetailUI>(Tapped);
        async void Tapped(OrderDetailUI obj)
        {
            await NavigationService.NavigateToAsync<DishDetailViewModel>(new NavigationParameters
            {
                {"Food",obj.Dish }
            });
        }
        decimal _orderTotalAmount;
        public decimal OrderTotalAmount
        {
            get => _orderTotalAmount;
            set => SetProperty(ref _orderTotalAmount, value);
        }
        ObservableCollection<OrderDetailUI> _orderDetailUIs;
        public ObservableCollection<OrderDetailUI> OrderDetailUIs
        {
            get => _orderDetailUIs;
            set => SetProperty(ref _orderDetailUIs, value);
        }

        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            parameters.TryGetValue("listorderdetails", out var orderDetailUIs);
            parameters.TryGetValue("tongtien", out var orderTotalAmount);
            parameters.TryGetValue("orderstatus", out var status);
            OrderTotalAmount = (decimal)orderTotalAmount;
            OrderDetailUIs = new ObservableCollection<OrderDetailUI>(orderDetailUIs as List<OrderDetailUI>);
            OrderStatus = (int)status;
            return Task.CompletedTask;
        }
        void Purchase()
        {

        }
    }
}
