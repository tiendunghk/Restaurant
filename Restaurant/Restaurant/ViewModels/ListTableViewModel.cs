using Restaurant.Mvvm.Command;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Restaurant.ViewModels
{
    public class ListTableViewModel: ViewModelBase
    {
        ObservableCollection<int> _tables;
        DelegateCommand<object> _tableTapped;
        public ObservableCollection<int> Tables
        {
            get => _tables;
            set => SetProperty(ref _tables, value);
        }
        public DelegateCommand<object> TableTapped => _tableTapped ??= new DelegateCommand<object>(Tapped);
        public ListTableViewModel()
        {
            Tables = new ObservableCollection<int>();
            for (int i = 1; i < 20; i++)
                Tables.Add(i);
        }
        async void Tapped(object table)
        {
            await NavigationService.NavigateToAsync<TableDetailViewModel>();
        }
    }
}
