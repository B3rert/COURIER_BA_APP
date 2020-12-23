using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CourierBA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuDetailPage : MasterDetailPage
    {
        private List<Models.AplicionUserModel> AplicionUserModels;

        public MenuDetailPage(string items, string user)
        {
            InitializeComponent();
            home();

            //Aplicacion 
            Models.Aplicacion myDeserializedClassAplication = JsonConvert.DeserializeObject<Models.Aplicacion>(items);
            var tableString = JsonConvert.SerializeObject(myDeserializedClassAplication.Table);
            AplicionUserModels = JsonConvert.DeserializeObject<List<Models.AplicionUserModel>>(tableString);
            listMenu.ItemsSource = AplicionUserModels;

        }

        private void home()
        {
            Detail = new NavigationPage(new HomePage());
        }

        private async void btnLogout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
            //await Navigation.PushAsync(new NavigationPage(new LoginPage())); //Push the page you want to push

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
            */
            //        await Navigation.PopToRootAsync();

        }
    }
}