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
    public partial class ListFoodManagerView : ContentPage
    {
        public ListFoodManagerView()
        {
            InitializeComponent();
            BindingContext = new ListFoodManagerViewModel();
        }
    }
}