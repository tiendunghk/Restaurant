using Restaurant.Mvvm.Command;
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
    }
}