using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{
    public class TableBillViewModel : ViewModelBase
    {
        List<int> _listItems;
        public List<int> ListItems
        {
            get => _listItems;
            set => SetProperty(ref _listItems, value);
        }
        public TableBillViewModel()
        {
            ListItems = new List<int>();
            for (int i = 0; i < 10; i++)
                ListItems.Add(i);
        }
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            string v;
            parameters.TryGetValue("title", out v);
            Title = v;
            Title = "Hóa đơn " + Title.ToLower();
            return Task.CompletedTask;
        }
    }
}
