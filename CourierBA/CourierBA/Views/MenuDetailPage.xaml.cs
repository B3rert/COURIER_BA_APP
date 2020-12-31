using Acr.UserDialogs;
using CourierBA.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CourierBA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuDetailPage : MasterDetailPage
    {
        MenuViewModel VM = new MenuViewModel();
        private List<Models.AplicionUserModel> AplicionUserModels;
        private List<Models.PA_bsc_User_Display_2Model> pA_Bsc_User_Display_2Models;
        string _user = null;
        public MenuDetailPage(string items, string user)
        {
            _user = user;
            InitializeComponent();
            BindingContext = VM;
            home();
        }

        private void home()
        {
            Detail = new NavigationPage(new HomePage());
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