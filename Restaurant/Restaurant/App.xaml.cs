using Restaurant.Models;
using Restaurant.Services.Dialog;
using Restaurant.ViewModels;
using Restaurant.ViewModels.Manager;
using Restaurant.ViewModels.Order;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant
{
    public partial class App : Application
    {
        public static AppContext Context { get; set; } = new AppContext();
        //app context chứa data load từ api về để set data cho shell
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzQwMTAxQDMxMzgyZTMzMmUzMG9LN01CMkxyTVdjYnJDS2Q3U05MWUpyVjhPd3JZTWwzTU9nSDl4MzFVc1k9");
            InitializeComponent();

            BuildDependencies();
            MainPage = new AppShell();

            for (int i = 1; i < 10; i++)
                Context.Tables.Add(new Table
                {
                    Id = Guid.NewGuid().ToString("N"),
                    TableName = "Bàn số " + i,
                    Status = (TableStatus)(i % 3)
                });

            //InitNavigation();
        }
        void BuildDependencies()
        {
            if (ServiceLocator.Instance.Built)
                return;

            // Register dependencies
            ServiceLocator.Instance.RegisterInstance<Services.Navigation.INavigationService, Services.Navigation.NavigationService>();
            ServiceLocator.Instance.RegisterInstance<IDialogService, DialogService>();

            ServiceLocator.Instance.RegisterViewModels();

            ServiceLocator.Instance.Build();
        }

        void InitNavigation()
        {
            ServiceLocator.Instance.Resolve<Services.Navigation.INavigationService>()
                .NavigateToAsync<ListFoodManagerViewModel>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
