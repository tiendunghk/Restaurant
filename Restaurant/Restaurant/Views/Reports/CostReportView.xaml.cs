using Restaurant.Services;
using Restaurant.Services.Dialog;
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
    public partial class CostReportView : ContentPage
    {
        public CostReportView()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var dialog = ServiceLocator.Instance.Resolve<IDialogService>();
            var path = await FileService.PickImageAsync(dialog);
            img.Source = ImageSource.FromFile(path);
            var xxx = await FileService.UploadImageCloudinary(path);
        }
    }
}