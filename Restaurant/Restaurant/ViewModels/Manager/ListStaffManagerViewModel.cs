using Restaurant.Datas;
using Restaurant.Models;
using Restaurant.Mvvm.Command;
using Restaurant.Services;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels.Manager
{
    public class ListStaffManagerViewModel : ViewModelBase
    {
        DelegateCommand _navigateCommand;
        DelegateCommand _searchCommand;
        public DelegateCommand NavigateCommand => _navigateCommand ??= new DelegateCommand(AddNavigate);
        DelegateCommand<Staff> _tappedCommand;
        public DelegateCommand<Staff> TappedCommand => _tappedCommand ??= new DelegateCommand<Staff>(Tapped);
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
        public ListStaffManagerViewModel()
        {
            Task.Run(async () =>
            {
                var listStaffs = await HttpService.GetAsync<List<Staff>>(Configuration.Api("staff/getall"));
                ListStaffs = listStaffs;
                BackupStaffs = ListStaffs;
            });
        }
        async void AddNavigate()
        {
            await NavigationService.NavigateToAsync<AddStaffViewModel>();
        }
        async void Tapped(Staff obj)
        {
            var parameters = new NavigationParameters();
            parameters.Add("Staff", obj);
            await NavigationService.NavigateToAsync<StaffDetailViewModel>(parameters);
        }
        async void Search()
        {
            IsSearch = true;
            var unicodeKeyword = Helpers.RemoveSign4VietnameseString(SearchKeyWord).ToLower();
            ListStaffs = new List<Staff>();
            await Task.Delay(1000);
            IsSearch = false;
            ListStaffs = new List<Staff>(BackupStaffs.Where(x => Helpers.RemoveSign4VietnameseString(x.Name).ToLower().Contains(unicodeKeyword)));
        }
    }
}
