using Restaurant.Models;
using Restaurant.Mvvm.Command;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;

namespace Restaurant.ViewModels
{
    public class ListTableViewModel : ViewModelBase
    {
        List<Table> _tables;
        DelegateCommand<Table> _tableTapped;
        public List<Table> Tables
        {
            get => _tables;
            set => SetProperty(ref _tables, value);
        }
        public DelegateCommand<Table> TableTapped => _tableTapped ??= new DelegateCommand<Table>(Tapped);
        public ListTableViewModel()
        {
            Tables = App.Context.Tables;
            //Tables = new List<Table>();
            //for (int i = 1; i < 20; i++)
            //    Tables.Add(new Table { 
            //        Id = Guid.NewGuid().ToString("N"), 
            //        TableName = "Bàn số " + i, 
            //        Status = (TableStatus)(i % 3) });
        }
        async void Tapped(Table table)
        {
            var parameters = new NavigationParameters();
            parameters.Add("title", table.TableName);
            await NavigationService.NavigateToAsync<TableDetailViewModel>(parameters);
        }
    }
}
