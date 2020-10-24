using Restaurant.Models;
using Restaurant.Mvvm.Command;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.ViewModels
{
    public class KitchenListFoodViewModel : ViewModelBase
    {
        DelegateCommand<string> _callWaiterCommand;
        public DelegateCommand<string> CallWaiterCommand => _callWaiterCommand ??= new DelegateCommand<string>(CallWaiter);
        List<FoodHeaderInfo> _listItems;
        public List<FoodHeaderInfo> ListItems
        {
            get => _listItems;
            set => SetProperty(ref _listItems, value);
        }
        public KitchenListFoodViewModel()
        {
            SetData();
        }
        void SetData()
        {
            ListItems = new List<FoodHeaderInfo>();
            FoodHeaderInfo obj = new FoodHeaderInfo();
            obj.Header = "Bàn 2";
            obj.Add(new Dish { Id = Guid.NewGuid().ToString("N"), Name = "món ăn", Price = 10000, DishImage = "com_tam" });
            obj.Add(new Dish { Id = Guid.NewGuid().ToString("N"), Name = "món ăn", Price = 100000, DishImage = "com_tam" });
            obj.Add(new Dish { Id = Guid.NewGuid().ToString("N"), Name = "món ăn", Price = 9000, DishImage = "com_tam" });
            ListItems.Add(obj);
            obj = new FoodHeaderInfo();
            obj.Header = "Bàn 1";
            obj.Add(new Dish { Id = Guid.NewGuid().ToString("N"), Name = "món ăn", Price = 1000, DishImage = "com_tam" });
            obj.Add(new Dish { Id = Guid.NewGuid().ToString("N"), Name = "món ăn", Price = 2000, DishImage = "com_tam" });
            ListItems.Add(obj);
            obj = new FoodHeaderInfo();
            obj.Header = "Bàn 12";
            obj.Add(new Dish { Id = Guid.NewGuid().ToString("N"), Name = "món ăn xxx", Price = 80000, DishImage = "com_tam" });
            ListItems.Add(obj);
            obj = new FoodHeaderInfo();
            obj.Header = "Bàn 8";
            obj.Add(new Dish { Id = Guid.NewGuid().ToString("N"), Name = "món ăn", Price = 10000, DishImage = "com_tam" });
            obj.Add(new Dish { Id = Guid.NewGuid().ToString("N"), Name = "món ăn", Price = 10000, DishImage = "com_tam" });
            obj.Add(new Dish { Id = Guid.NewGuid().ToString("N"), Name = "món ăn", Price = 10000, DishImage = "com_tam" });
            ListItems.Add(obj);
            obj = new FoodHeaderInfo();
            obj.Header = "Bàn 6";
            obj.Add(new Dish { Id = Guid.NewGuid().ToString("N"), Name = "món ăn", Price = 10000, DishImage = "com_tam" });
            obj.Add(new Dish { Id = Guid.NewGuid().ToString("N"), Name = "món ăn", Price = 10000, DishImage = "com_tam" });
            obj.Add(new Dish { Id = Guid.NewGuid().ToString("N"), Name = "món ăn", Price = 10000, DishImage = "com_tam" });
            ListItems.Add(obj);
        }
        void CallWaiter(string str)
        {
            foreach (var ele in ListItems)
            {
                foreach (var e in ele)
                {
                    if (e.Id == str)
                    {
                        ele.Remove(e);
                        if (ele==null)
                            ListItems.Remove(ele);
                        DialogService.ShowToast("Đã thông báo cho waiter!");
                        return;
                    }
                }
            }
        }
    }
}
