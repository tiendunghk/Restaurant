using Restaurant.ViewModels.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Views.Manager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddStaffView : ContentPage
    {
        public AddStaffView()
        {
            InitializeComponent();
            BindingContext = new AddStaffViewModel();
        }
    }
}