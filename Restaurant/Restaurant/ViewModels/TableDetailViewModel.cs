using Restaurant.Mvvm.Command;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{
    public class TableDetailViewModel : ViewModelBase
    {
        DelegateCommand<string> _fabCommand;
        DelegateCommand<object> _tappedCommand;
        public DelegateCommand<object> TappedCommand => _tappedCommand ??= new DelegateCommand<object>(Tapped);
        public DelegateCommand<string> FABCommand => _fabCommand ??= new DelegateCommand<string>(FABDoing);
        List<string> _tests;
        public List<string> Tests
        {
            get => _tests;
            set => SetProperty(ref _tests, value);
        }
        public TableDetailViewModel()
        {
            Tests = new List<string>();
            for (int i = 0; i < 10; i++)
                Tests.Add("món " + i);
        }
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            string v = "";
            parameters.TryGetValue("title", out v);
            Title = v;
            return Task.CompletedTask;
        }
        void Tapped(object o)
        {

        }
        async void FABDoing(string title)
        {
            var parameters = new NavigationParameters();
            parameters.Add("title", title);
            int a=await DialogService.DisplayActionSheetAsync("Tính năng", "Hủy", null, "Tạo hóa đơn", "Yêu cầu thanh toán");
            if (a == 0)
                await NavigationService.NavigateToAsync<TableBillViewModel>(parameters);
        }
    }
}
