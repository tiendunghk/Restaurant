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
    }
}