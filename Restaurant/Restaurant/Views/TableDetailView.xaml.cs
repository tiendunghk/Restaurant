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

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //radio1.IsChecked = true ? true : false;
            //radio2.IsChecked = false;
            //radio3.IsChecked = false;
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            //radio2.IsChecked = true ? true : false;
            //radio1.IsChecked = false;
            //radio3.IsChecked = false;
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            //radio3.IsChecked = true ? true : false;
            //radio1.IsChecked = false;
            //radio2.IsChecked = false;
        }
    }
}