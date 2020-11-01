using Restaurant.Models;
using Restaurant.Mvvm.Command;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels
{
    public class TableDetailViewModel : ViewModelBase
    {
        OrderModel order = null;
        DelegateCommand<string> _fabCommand;
        DelegateCommand<object> _tappedCommand;
        public DelegateCommand<object> TappedCommand => _tappedCommand ??= new DelegateCommand<object>(Tapped);
        ObservableCollection<Dish> _tests;
        public ObservableCollection<Dish> Tests
        {
            get => _tests;
            set => SetProperty(ref _tests, value);
        }



        

        ObservableCollection<OrderDetailUI> _orderedItems;
        public ObservableCollection<OrderDetailUI> OrderedItems
        {
            get => _orderedItems;
            set => SetProperty(ref _orderedItems, value);
        }

        List<string> _filters;
        public List<string> Filters
        {
            get => _filters;
            set => SetProperty(ref _filters, value);
        }
        int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetProperty(ref _selectedIndex, value);
        }
        public TableDetailViewModel()
        {
            Filters = new List<string>
            {
                "Đang nấu","Đã phục vụ","Đang chờ"
            };
            SelectedIndex = 0;
            Tests = new ObservableCollection<Dish>();
            for (int i = 0; i < 10; i++)
                Tests.Add(new Dish
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "Món " + i,
                    Description = "Quá hấp dẫn",
                    Price = 30000,
                    DishImage = "com_tam.jpg",
                });
            OrderedItems = new ObservableCollection<OrderDetailUI>();
        }
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            string v = "";
            parameters.TryGetValue("title", out v);
            Title = v;
            return Task.CompletedTask;
        }
        void Tapped(object o)
        {

        }
        DelegateCommand _submitCommand;
        public DelegateCommand SubmitCommand => _submitCommand ??= new DelegateCommand(Submit);
        void Submit()
        {
            var a = Tests;
            if (order == null) order = new OrderModel { Id = Guid.NewGuid().ToString("N") };
            foreach (var elem in Tests)
            {
                if (elem.SoLuong > 0)
                {
                    for (int i = 0; i < elem.SoLuong; i++)
                    {
                        OrderedItems.Add(new OrderDetailUI
                        {
                            OrderDetail = new OrderDetail
                            {
                                OrderDetailId = Guid.NewGuid().ToString("N"),
                                DishId = elem.Id
                            },
                            NameDish = elem.Name,
                            ImageUrl = elem.DishImage,
                            Status = OrderDetailStatus.WAITING
                        });

                    }
                    elem.SoLuong = 0;
                }
            }
        }
    }
}
