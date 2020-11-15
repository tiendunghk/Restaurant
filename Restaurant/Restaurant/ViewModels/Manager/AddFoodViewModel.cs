using Restaurant.Models;
using Restaurant.Mvvm.Command;
using Restaurant.Services;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ViewModels.Manager
{
    public class AddFoodViewModel : ViewModelBase
    {
        DelegateCommand<Dish> _removeCommand;
        DelegateCommand<Dish> _pickCommand;
        DelegateCommand _saveCommand;
        Dish _dish;
        public Dish Obj
        {
            get => _dish;
            set => SetProperty(ref _dish, value);
        }
        bool _nullImage;
        public bool NullImage
        {
            get => _nullImage;
            set => SetProperty(ref _nullImage, value);
        }
        public DelegateCommand<Dish> RemoveCommand => _removeCommand ??= new DelegateCommand<Dish>(Remove);
        public DelegateCommand<Dish> PickCommand => _pickCommand ??= new DelegateCommand<Dish>(Pick);
        public DelegateCommand SaveCommand => _saveCommand ??= new DelegateCommand(Save);
        public AddFoodViewModel()
        {
            Obj = new Dish();
            NullImage = true;
        }
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            return base.OnNavigationAsync(parameters, navigationType);
        }
        void Remove(Dish dish)
        {
            dish.DishImage = string.Empty;
            NullImage = true;
        }
        async void Pick(Dish dish)
        {
            var path = await FileService.PickImageAsync(DialogService);
            dish.DishImage = path;
            NullImage = false;
        }
        void Save()
        {

        }
    }
}
