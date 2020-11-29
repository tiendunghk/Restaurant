using Restaurant.Datas;
using Restaurant.Models;
using Restaurant.Services;
using Restaurant.ViewModels.Manager;
using Restaurant.ViewModels.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Views.Reports
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodReportView : ContentPage
    {
        public FoodReportView()
        {
            InitializeComponent();
            BindingContext = new FoodReportViewModel();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var listStaff = HttpService.GetAsync<List<Staff>>(Configuration.Api("staff/getall"));
            var listDish = HttpService.GetAsync<List<Dish>>(Configuration.Api("dish/getall"));
            var listOrder = HttpService.GetAsync<List<OrderModel>>(Configuration.Api("order/getall"));
            var listOrderDetail = HttpService.GetAsync<List<OrderDetail>>(Configuration.Api("orderdetail/getall"));
            var listTable = HttpService.GetAsync<List<Table>>(Configuration.Api("table/getall"));
        }
    }
}