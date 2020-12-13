using Newtonsoft.Json;
using Restaurant.Datas;
using Restaurant.Models;
using Restaurant.Services;
using Restaurant.ViewModels.Manager;
using Restaurant.ViewModels.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
            lblMonth.Text = $"Tháng {DateTime.Now.Month}";
        }
        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var vm = BindingContext as FoodReportViewModel;
            vm.ComboBoxChanged();
        }
    }
}