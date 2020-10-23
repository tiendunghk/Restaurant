using Restaurant.Services.Dialog;
using Restaurant.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzQwMTAxQDMxMzgyZTMzMmUzMG9LN01CMkxyTVdjYnJDS2Q3U05MWUpyVjhPd3JZTWwzTU9nSDl4MzFVc1k9");
            InitializeComponent();

            BuildDependencies();

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
                .NavigateToAsync<LoginViewModel>();
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
