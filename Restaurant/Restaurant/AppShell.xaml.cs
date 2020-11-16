﻿using Restaurant.Models;
using Restaurant.Mvvm;
using Restaurant.Mvvm.Command;
using Restaurant.Services.Dialog;
using Restaurant.ViewModels;
using Restaurant.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public Staff Staff { get; set; }
        DelegateCommand _logoutCommand;
        public DelegateCommand LogoutCommand => _logoutCommand ??= new DelegateCommand(Logout);
        public AppShell()
        {
            InitializeComponent();
            Staff = App.Context.CurrentStaff;
            BindingContext = this;
            AppShell.SetTabBarIsVisible(this, false);
        }
        async void Logout()
        {
            var answer = await ServiceLocator.Instance.Resolve<IDialogService>().ShowConfirmDialog("Warning", "Bạn có chắc chắn muốn thoát ứng dụng?", "OK", "Cancel");
            if (answer)
            {
                Application.Current.MainPage = new LoginView();
            }

        }
        protected override async void OnNavigating(ShellNavigatingEventArgs args)
        {
            //call api to load data here
            //base.OnNavigating(args);
            if (args.Target.Location.OriginalString.Contains("2"))
            {
                //Table
                App.Context.TableClickCount++;
                if (App.Context.TableClickCount <= 1)
                    MessagingCenter.Send("abc", "LoadDataTable");
            }
            if (args.Target.Location.OriginalString.Contains("4"))
            {
                //order
                App.Context.OrderClickCount++;
                if (App.Context.OrderClickCount <= 1)
                    MessagingCenter.Send("abc", "LoadDataOrder");
            }
            if (args.Target.Location.OriginalString.Contains("6"))
            {
                if (App.Context.CurrentStaff.Role != "Kitchen")
                {
                    args.Cancel();
                    await ServiceLocator.Instance.Resolve<IDialogService>().ShowAlertAsync("Bạn không có quyền truy cập", "Cảnh báo", "OK");
                }
                else
                {
                    //kitchen
                    App.Context.KitchenClickCount++;
                    if (App.Context.KitchenClickCount <= 1)
                        MessagingCenter.Send("abc", "LoadDataKitchen");
                }

            }
            if (args.Target.Location.OriginalString.Contains("8"))
            {
                if (App.Context.CurrentStaff.Role != "Manager")
                {
                    args.Cancel();
                    await ServiceLocator.Instance.Resolve<IDialogService>().ShowAlertAsync("Bạn không có quyền truy cập", "Cảnh báo", "OK");
                }
                else
                {
                    //manager
                }

            }
            if (args.Target.Location.OriginalString.Contains("10"))
            {
                if (App.Context.CurrentStaff.Role != "Manager")
                {
                    args.Cancel();
                    await ServiceLocator.Instance.Resolve<IDialogService>().ShowAlertAsync("Bạn không có quyền truy cập", "Cảnh báo", "OK");
                }
                else
                {
                    //report
                }

            }
        }
        protected override bool OnBackButtonPressed()
        {
            Current.Navigation.PopAsync(true);
            return true;
        }
    }
}