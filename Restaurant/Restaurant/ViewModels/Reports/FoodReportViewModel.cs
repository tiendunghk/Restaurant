using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels.Reports
{
    public class FoodReportViewModel: ViewModelBase
    {
        public FoodReportViewModel()
        {

        }
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            return base.OnNavigationAsync(parameters, navigationType);
        }
    }
}
