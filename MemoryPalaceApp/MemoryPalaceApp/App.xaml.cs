using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MemoryPalaceApp.Services;
using MemoryPalaceApp.Views;
using Autofac;

namespace MemoryPalaceApp
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string AzureBackendUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://192.168.180.70:5001" : "https://192.168.180.70:5001";
        public static bool UseMockDataStore = false;
        static IContainer container;
        static readonly ContainerBuilder builder = new ContainerBuilder();

        public App()
        {
            InitializeComponent();
            
            builder.RegisterModule<ConfigurationModule>();
            container = builder.Build();

            Xamarin.Forms.Internals.DependencyResolver.ResolveUsing(type => container.IsRegistered(type) ? container.Resolve(type) : null);

            string configFromAppSettingsManager = AppSettingsManager.Settings["Service"];
            Console.WriteLine($"Service url from AppSettingsManager: {configFromAppSettingsManager}");

            var config = container.Resolve<IConfiguration>();
            Console.WriteLine($"Service from ConfigurationModule: {config.ApiBaseAddress}");

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();
            MainPage = new MainPage();
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
