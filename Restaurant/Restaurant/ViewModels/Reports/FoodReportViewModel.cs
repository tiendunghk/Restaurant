using Restaurant.Datas;
using Restaurant.Models;
using Restaurant.Services;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels.Reports
{
    public class FoodReportViewModel : ViewModelBase
    {
        List<string> _pickers;
        public List<string> Pickers
        {
            get => _pickers;
            set => SetProperty(ref _pickers, value);
        }
        int _selectedIndex;
        public int SelectedIndex { get => _selectedIndex; set => SetProperty(ref _selectedIndex, value); }
        DateTime _date;
        public DateTime Date { get => _date; set => SetProperty(ref _date, value); }

        List<Dish> _listDishs;
        public List<Dish> ListDishs { get => _listDishs; set => SetProperty(ref _listDishs, value); }
        public FoodReportViewModel()
        {
            Pickers = new List<string>
            {
                "Tất cả",
                "Được order nhiều nhất",
                "Được order ít nhất",
                "Doanh thu cao nhất",
                "Doanh thu thấp nhất"
            };
            SelectedIndex = 0;
            Date = DateTime.Now;
        }
        public override async Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            ListDishs = await HttpService.GetAsync<List<Dish>>(Configuration.Api("dish/getall/true"));
        }
        public void DateChanged()
        {
            var a = Date;
        }
        public void ComboBoxChanged()
        {
            var b = SelectedIndex;
        }
    }
}
