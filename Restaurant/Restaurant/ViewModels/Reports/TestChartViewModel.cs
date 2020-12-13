using Microcharts;
using Restaurant.Datas;
using Restaurant.Models;
using Restaurant.Services;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels.Reports
{
    public class TestChartViewModel : ViewModelBase
    {
        private BarChart barChart;
        public BarChart BarChart
        {
            get => barChart;
            set => SetProperty(ref barChart, value);
        }
        List<Dish> _listOrders;
        public List<Dish> ListOrders { get => _listOrders; set => SetProperty(ref _listOrders, value); }
        public TestChartViewModel()
        {

        }
        public override async Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            ListOrders = await HttpService.GetAsync<List<Dish>>(Configuration.Api($"dish/getall/false"));
        }
    }
}
