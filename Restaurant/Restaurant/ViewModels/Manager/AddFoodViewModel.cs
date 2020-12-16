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
    public class AddFoodViewModel : ViewModelBase
    {
        DelegateCommand<Dish> _removeCommand;
        DelegateCommand<Dish> _pickCommand;
        DelegateCommand _saveCommand;
        DelegateCommand _cancelCommand;
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
        public DelegateCommand CancelCommand => _cancelCommand ??= new DelegateCommand(Cancel);
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
        async void Save()
        {
            if (!Obj.Validate())
            {
                await DialogService.ShowAlertAsync("Kiểm tra lại input nhé!", "Error", "OK");
                return;
            }
            using (UserDialogs.Instance.Loading("Saving", null, null, false))
            {
                if (!string.IsNullOrEmpty(Obj.DishImage))
                {
                    var url = await FileService.UploadImageCloudinary(Obj.DishImage);
                    Obj.DishImage = url;
                }
                await HttpService.PostApiAsync<object>(Configuration.Api("dish/add"), Obj);
                DialogService.ShowToast("Đã thêm thành công");
                MessagingCenter.Send("abc", "AddFoodDone");
                await NavigationService.NavigateBackAsync();
            }
        }
        async void Cancel()
        {
            await NavigationService.NavigateBackAsync();
        }
    }
}
