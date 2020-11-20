using Restaurant.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Views.Order
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListOrderView : ContentPage
    {
        public ListOrderView()
        {
            InitializeComponent();
            BindingContext = new ListOrderViewModel();
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var vm = BindingContext as ListOrderViewModel;
            vm.PickerChanged();
        }
    }
}