﻿using Restaurant.Datas;
using Restaurant.Models;
using Restaurant.Mvvm.Command;
using Restaurant.Services;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
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
            //Tables = App.Context.Tables;
            //Tables = Datas.Tables.ListTables;
            MessagingCenter.Subscribe<string>("abc", "LoadDataTable", async (a) =>
            {
                IsLoadingData = true;
                var listTables = await HttpService.GetAsync<List<Table>>(Configuration.Api("table/getall/true"));
                Tables = listTables;
                IsLoadingData = false;
            });
        }
        async void Tapped(Table table)
        {
            if (App.Context.CurrentStaff.RoleName == "Cashier" || App.Context.CurrentStaff.RoleName == "Kitchen")
            {
                await DialogService.ShowAlertAsync("Bạn không có quyền truy cập", "Thông báo", "OK");
                return;
            }
            var parameters = new NavigationParameters();
            var listDishs = await HttpService.GetAsync<List<Dish>>(Configuration.Api("dish/getall/true"));
            Datas.Dishs.ListDishs = new ObservableCollection<Dish>(listDishs);
            parameters.Add("title", table.TableName);
            parameters.Add("table", table);
            await NavigationService.NavigateToAsync<TableDetailViewModel>(parameters);
        }
    }
}
