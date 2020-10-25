using Restaurant.Mvvm.Command;
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
        DelegateCommand _logoutCommand;
        public DelegateCommand LogoutCommand => _logoutCommand ??= new DelegateCommand(Logout);
        public AppShell()
        {
            InitializeComponent();
            BindingContext = this;
            AppShell.SetTabBarIsVisible(this, false);
        }
        void Logout()
        {
            Application.Current.MainPage = new LoginView();
        }
        protected override async void OnNavigating(ShellNavigatingEventArgs args)
        {
            //call api to load data here
            base.OnNavigating(args);
            if (args.Target.Location.OriginalString.Contains("4"))
            {
                //order
                MessagingCenter.Send("abc", "LoadDataOrder");
            }
            if (args.Target.Location.OriginalString.Contains("6"))
            {
                //kitchen
                App.Context.KitchenClickCount++;
                if (App.Context.KitchenClickCount <= 1)
                    MessagingCenter.Send("abc", "LoadDataKitchen");
            }
        }
    }
}