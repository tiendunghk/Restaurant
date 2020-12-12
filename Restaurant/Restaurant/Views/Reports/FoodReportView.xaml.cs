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
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var _referenceObj = App.Context.CurrentStaff.Clone() as Staff;
            _referenceObj.Name = "fucker";
            //await HttpService.PostApiAsync<object>(Configuration.Api("staff/update"), _referenceObj);
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _referenceObj.Token);
            var json = JsonConvert.SerializeObject(_referenceObj);
            var a = await httpClient.PostAsJsonAsync(Configuration.Api("staff/update"), _referenceObj);
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            var vm = BindingContext as FoodReportViewModel;
            vm.DateChanged();
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var vm = BindingContext as FoodReportViewModel;
            vm.ComboBoxChanged();
        }
    }
}