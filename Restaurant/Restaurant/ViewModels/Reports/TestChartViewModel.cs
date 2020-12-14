using Microcharts;
using Restaurant.Datas;
using Restaurant.Models;
using Restaurant.Mvvm.Command;
using Restaurant.Services;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels.Reports
{
    public class TestChartViewModel : ViewModelBase
    {
        DelegateCommand _navigateCommand;
        DelegateCommand _searchCommand;
        public DelegateCommand SearchCommand => _searchCommand ??= new DelegateCommand(Search);
        List<Staff> _listStaffs;
        public List<Staff> ListStaffs
        {
            get => _listStaffs;
            set => SetProperty(ref _listStaffs, value);
        }
        public List<Staff> BackupStaffs { get; set; }
        bool _isSearch;
        public bool IsSearch
        {
            get => _isSearch;
            set => SetProperty(ref _isSearch, value);
        }
        string _searchKeyWord;
        public string SearchKeyWord
        {
            get => _searchKeyWord;
            set
            {
                SetProperty(ref _searchKeyWord, value);
                if (string.IsNullOrEmpty(value))
                    ListStaffs = BackupStaffs;
            }
        }
        public TestChartViewModel()
        {

        }
        public override async Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            ListStaffs = BackupStaffs = await HttpService.GetAsync<List<Staff>>(Configuration.Api("staff/getall/false"));
        }

        async void Search()
        {
            IsSearch = true;
            var unicodeKeyword = Helpers.RemoveSign4VietnameseString(SearchKeyWord).ToLower();
            ListStaffs = new List<Staff>();
            await Task.Delay(1000);
            IsSearch = false;
            ListStaffs = new List<Staff>(BackupStaffs.Where(x => Helpers.RemoveSign4VietnameseString(x.Name).ToLower().Contains(unicodeKeyword)));
            RaisePropertyChanged(nameof(ListStaffs));
        }
    }
}
