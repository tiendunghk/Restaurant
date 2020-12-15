using Acr.UserDialogs;
using Restaurant.Datas;
using Restaurant.Models;
using Restaurant.Mvvm.Command;
using Restaurant.Services;
using Restaurant.Services.Navigation;
using Restaurant.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Restaurant.ViewModels.Manager
{
    public class FoodDetailViewModel : ViewModelBase
    {
        bool _nullImage;
        public bool NullImage
        {
            get => _nullImage;
            set => SetProperty(ref _nullImage, value);
        }
        DelegateCommand _cancelCommand;
        DelegateCommand _saveCommand;
        DelegateCommand<Dish> _removeCommand;
        DelegateCommand<Dish> _pickCommand;
        Dish _obj, _referenceObj;
        public Dish Obj
        {
            get => _obj;
            set => SetProperty(ref _obj, value);
        }
        public DelegateCommand CancelCommand => _cancelCommand ??= new DelegateCommand(Cancel);
        public DelegateCommand SaveCommand => _saveCommand ??= new DelegateCommand(Save);
        public DelegateCommand<Dish> RemoveCommand => _removeCommand ??= new DelegateCommand<Dish>(Remove);
        public DelegateCommand<Dish> PickCommand => _pickCommand ??= new DelegateCommand<Dish>(Pick);

        public FoodDetailViewModel()
        {

        }
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            parameters.TryGetValue("Food", out _referenceObj);
            Obj = _referenceObj.Clone() as Dish;
            return Task.CompletedTask;
        }
        async void Cancel()
        {
            await NavigationService.NavigateBackAsync();
        }
        async void Save()
        {
            using (UserDialogs.Instance.Loading("Saving..."))
            {
                _referenceObj.IsActive = Obj.IsActive;
                _referenceObj.Name = Obj.Name;
                if (!Obj.DishImage.Contains("http"))
                {
                    var urlImage = await FileService.UploadImageCloudinary(Obj.DishImage);
                    _referenceObj.DishImage = urlImage;
                }
                _referenceObj.Description = Obj.Description;
                _referenceObj.Price = Obj.Price;

                await HttpService.PostApiAsync<object>(Configuration.Api("dish/update"), _referenceObj);
                MessagingCenter.Send("abc", "AddFoodDone");
                await NavigationService.NavigateBackAsync();
            }
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
    }
}
