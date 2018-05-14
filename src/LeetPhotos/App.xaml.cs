using Xamarin.Forms;
using TinyNavigationHelper.Forms;
using System.Reflection;
using Autofac;
using System;
using TinyNavigationHelper;
using LeetPhotos.Core.IoC;
using LeetPhotos.Core.ViewModel;
using TinyNavigationHelper.Abstraction;
using LeetPhotos.Core.Net;
using LeetPhotos.Core.Services;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using System.Windows.Input;

namespace LeetPhotos
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

			var navigationHelper = new FormsNavigationHelper(this);
			navigationHelper.RegisterViewsInAssembly(Assembly.GetExecutingAssembly());

			var builder = new ContainerBuilder();

			builder.RegisterInstance<INavigationHelper>(navigationHelper);

			// ViewModels
            var coreAssembly = Assembly.Load(new AssemblyName("LeetPhotos.Core"));
            builder.RegisterAssemblyTypes(coreAssembly)
                   .Where(x => x.Name.EndsWith("ViewModel", StringComparison.Ordinal));

			builder.RegisterType<RestClient>().As<IRestClient>();
			builder.RegisterType<PhotoService>().As<IPhotoService>();
			builder.RegisterType<Command>().As<ICommand>();

			var container = builder.Build();
			Resolver.SetResolver(new AutofacResolver(container));

			ViewModel.BeginInvokeOnMainThread = (action) => Device.BeginInvokeOnMainThread(action);

			NavigationHelper.Current.SetRootView("PhotosView");
            
			((Xamarin.Forms.NavigationPage)MainPage).On<Xamarin.Forms.PlatformConfiguration.iOS>().SetPrefersLargeTitles(true);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
