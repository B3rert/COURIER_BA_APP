using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CourierBA
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzc1NjYwQDMxMzgyZTM0MmUzMGYrUWVSNFB6SXRkaE9hTmJnMDNKdHJQc1N2UGFjOGVWNU43RWxMaTZBd1E9");
            InitializeComponent();

            MainPage = new NavigationPage(new Views.LoginPage());
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
