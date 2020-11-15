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
            var output = await HttpService.GetAsync<object>("http://192.168.137.1:5001/api/user");
        }
    }
}