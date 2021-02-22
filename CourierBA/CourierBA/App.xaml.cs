using CourierBA.Helpers;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CourierBA
{
    public partial class App : Application
    {

        static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = 
                        new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "productos.db3"));
                }
                return database;
            }
        }
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzc1NjYwQDMxMzgyZTM0MmUzMGYrUWVSNFB6SXRkaE9hTmJnMDNKdHJQc1N2UGFjOGVWNU43RWxMaTZBd1E9");
            InitializeComponent();

          MainPage = new NavigationPage(new Views.LoginPage());
          //  MainPage = new NavigationPage(new Views.DocumentoCourierPage());
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
