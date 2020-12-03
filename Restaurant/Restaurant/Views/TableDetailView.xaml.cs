using Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TableDetailView : ContentPage
    {
        public TableDetailView()
        {
            InitializeComponent();
        }

        private void stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var vm = BindingContext as TableDetailViewModel;
            vm.StepperValue();
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var vm = BindingContext as TableDetailViewModel;
            vm.PickerChanged();
        }

        private async void  tabView_TabItemTapped(object sender, Syncfusion.XForms.TabView.TabItemTappedEventArgs e)
        {
            if (e.TabItem.Title == "Món đã đặt")
            {
                var vm = BindingContext as TableDetailViewModel;
                await vm.LoadOrderDetailData();
            }
        }
    }
}