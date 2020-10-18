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

            ServiceLocator.Instance.RegisterViewModels();

            ServiceLocator.Instance.Build();
        }

        void InitNavigation()
        {
            ServiceLocator.Instance.Resolve<Services.Navigation.INavigationService>()
                .NavigateToAsync<Page1ViewModel>();
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
