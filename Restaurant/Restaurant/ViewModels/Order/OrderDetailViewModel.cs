using Acr.UserDialogs;
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
        public DelegateCommand PurchaseCommand => _purchaseCommand ??= new DelegateCommand(Purchase);
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
        async void Purchase()
        {
            if (!await DialogService.ShowConfirmDialog("Warning", "Bạn chắc chắn thanh toán", "Yes", "No"))
            {
                return;
            }
            using (UserDialogs.Instance.Loading("Waiting..."))
            {
                var order = await HttpService.GetAsync<OrderModel>(Configuration.Api($"order/{OrderDetailUIs[0].OrderDetail.OrderDetail_OrderID}"));
                var staffs = await HttpService.GetAsync<List<Staff>>(Configuration.Api("staff/getall/true"));
                var idpush = staffs.Where(x => x.Id == order.StaffId).Select(x => x.ExternalId).ToList();
                order.Status = Models.OrderStatus.COMPLETED;
                OrderStatus = (int)Models.OrderStatus.COMPLETED;
                await HttpService.PostApiAsync<object>(Configuration.Api("order/update"), order);

                //var table = order.TableId;
                var tables = await HttpService.GetAsync<List<Table>>(Configuration.Api($"table/getall/true"));
                var table = tables.Where(t => t.Id == order.TableId).FirstOrDefault();
                table.TableIdOrderServing = null;
                table.Status = TableStatus.DIRTY;
                await HttpService.PostApiAsync<object>(Configuration.Api("table/update"), table);
                Notification.PushExternalID(new { flag = NotiFlag.TABLESTATUS }, idpush, "Yêu cầu thanh toán đã được chấp nhận");
                await PushNotiCashier();
                MessagingCenter.Send("abc", "LoadDataOrder");
            }
        }
        async Task PushNotiCashier()
        {
            var listStaffs = await HttpService.GetAsync<List<Staff>>(Configuration.Api("staff/getall/true"));
            listStaffs = listStaffs.Where(x => x.Role == "2" || x.Role == "5").ToList();
            var externalIds = listStaffs.Select(x => x.ExternalId).ToList();
            Notification.SilentPush(externalIds, new { flag = NotiFlag.SENDTOCASHIER });
        }
    }
}
