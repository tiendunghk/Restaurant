using Newtonsoft.Json;
using Restaurant.Services;
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
    public partial class OrderReportView : ContentPage
    {
        public OrderReportView()
        {
            InitializeComponent();
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var vm = BindingContext as OrderReportViewModel;
            vm.ComboBoxChanged();
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            var vm = BindingContext as OrderReportViewModel;
            vm.DateChanged();
        }

        private void Picker_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}