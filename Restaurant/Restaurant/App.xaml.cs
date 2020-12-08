using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Restaurant.Models;
using Restaurant.Services;
using Restaurant.Services.Dialog;
using Restaurant.ViewModels;
using Restaurant.ViewModels.Manager;
using Restaurant.ViewModels.Order;
using System;
using Xamarin.Essentials;
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
            OneSignal.Current.StartInit("511f254e-f0fe-4353-856d-1ac41bee6027")
                .HandleNotificationReceived(Notification.HandleNotificationReceived)
                .HandleNotificationOpened(Notification.HandleNotificationOpened)
                .InFocusDisplaying(OSInFocusDisplayOption.Notification)
                .EndInit();

            BuildDependencies();
            //MainPage = new AppShell();
            InitNavigation();
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
                .NavigateToAsync<SplashViewModel>();
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=b607b36d-19ae-4416-949e-cece5515e5a5;",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
