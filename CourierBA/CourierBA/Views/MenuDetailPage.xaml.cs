using CourierBA.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CourierBA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuDetailPage : MasterDetailPage
    {
        MenuViewModel VM = new MenuViewModel();
        string _user = null;
        int? _empresa = 0;

        public MenuDetailPage( string user, int? empresa)
        {
            _empresa = empresa;
            _user = user;
            InitializeComponent();
            BindingContext = VM;
            home();
        }

        private void home()
        {
            Detail = new NavigationPage(new GuiaReferenciaPage(_empresa, _user));
        }

        private async void btnLogout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
            
            /*
            var existingPages = Navigation.NavigationStack.ToList();
            //get all the pages in the stack
            foreach (var page in existingPages)
            {
                //Check they type of the page if its not the 
                //same type as the newly created one remove it
                if (page.GetType() == typeof(LoginPage))

                    continue;

                Navigation.RemovePage(page);
            }
            
           
           await Navigation.PopToRootAsync();
            */
        }

       
        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM._user = _user;
            VM.LoadData();
        }
    }
}