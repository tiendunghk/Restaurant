using Restaurant.Controls;
using Restaurant.ViewModels.Base;
using Restaurant.Views;
using Restaurant.Views.Manager;
using Restaurant.Views.Order;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Restaurant.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        protected Application CurrentApplication => Application.Current;
        NavigableElement _navigationRoot;
        NavigableElement NavigationRoot => _navigationRoot ??= GetNavigationRoot();
        static NavigableElement GetNavigationRoot()
        {
            if (Application.Current.MainPage is Shell shell)
                return shell;

            return null;
        }
        public Task InitializeAsync()
        {
            //return NavigateToAsync<LoginViewModel>();
            return Task.CompletedTask;
        }


        public async Task NavigateToAsync(Type viewModelType, NavigationParameters parameters)
        {
            var view = FindViewByViewModel(viewModelType);
            if (view is LoginView || view is SplashView || NavigationRoot == null)
            {
                CurrentApplication.MainPage = view;
                _navigationRoot = null;
            }
            else
            {
                await NavigationRoot.Navigation.PushAsync(view);
            }

            if (view.BindingContext is ViewModelBase vm)
            {
                await vm.OnNavigationAsync(parameters, NavigationType.New);
            }
        }
        public async Task NavigateBackAsync(NavigationParameters parameters)
        {
            await NavigationRoot.Navigation.PopAsync(true);
        }

        public async Task NavigateBackToMainPageAsync()
        {
            if (!(CurrentApplication.MainPage is CustomNavigationPage))
                return;

            for (var i = CurrentApplication.MainPage.Navigation.NavigationStack.Count - 2; i > 0; i--)
                CurrentApplication.MainPage?.Navigation.RemovePage(CurrentApplication.MainPage.Navigation
                    .NavigationStack[i]);

            await CurrentApplication.MainPage.Navigation.PopAsync();
        }

        public Task NavigateBackAsync()
        {
            return NavigateBackAsync(null);
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return NavigateToAsync(typeof(TViewModel), new NavigationParameters());
        }

        public Task NavigateToAsync<TViewModel>(NavigationParameters parameters) where TViewModel : ViewModelBase
        {
            return NavigateToAsync(typeof(TViewModel), parameters);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return NavigateToAsync(viewModelType, new NavigationParameters());
        }



        protected Page FindViewByViewModel(Type viewModelType)
        {
            try
            {
                var viewType = Type.GetType(viewModelType.FullName.Replace("ViewModel", "View"));

                if (viewType == null)
                    throw new Exception($"Mapping type for {viewModelType} is not a page");

                var view = Activator.CreateInstance(viewType) as Page;

                if (view != null)
                {
                    var a = ServiceLocator.Instance.Resolve(viewModelType);
                    if (view.BindingContext == null)
                        view.BindingContext = ServiceLocator.Instance.Resolve(viewModelType);
                }

                return view;
            }
            catch (Exception ex)
            {
                Debugger.Break();

                throw;
            }
        }
    }
}
