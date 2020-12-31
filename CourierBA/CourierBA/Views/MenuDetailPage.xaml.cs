using Acr.UserDialogs;
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
        private List<Models.AplicionUserModel> AplicionUserModels;
        private List<Models.PA_bsc_User_Display_2Model> pA_Bsc_User_Display_2Models;

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

        }

        //Aplication select
        int? aplicationSelect = null;
        private async void listMenu_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            //var apliaction = e.Item as Models.AplicionUserModel;
            //aplicationSelect = apliaction.Application.Value;

            //api display
            UserDialogs.Instance.ShowLoading(title: "Cargando...");
            try
            {
                
                //consumo api rest
                HttpClient client = new HttpClient();
                client.BaseAddress = Global.GlobalVariables.Servidor;
                string url = string.Format("/api/PA_bsc_User_Display_2"); //URL API
                var response = await client.GetAsync(url);
                var  result = response.Content.ReadAsStringAsync().Result;

                //listar datos
                Models.PA_bsc_User_Display_2 myDeserializedClass = 
                    JsonConvert.DeserializeObject<Models.PA_bsc_User_Display_2>(result);
                var tableString = JsonConvert.SerializeObject(myDeserializedClass.Table);
                pA_Bsc_User_Display_2Models = 
                    JsonConvert.DeserializeObject<List<Models.PA_bsc_User_Display_2Model>>(tableString);





//               listMenu.ItemsSource = AplicionUserModels;




                UserDialogs.Instance.HideLoading();

               
            }
            catch
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Error", "No se ha podido conectar con el servidor", "Aceptar");
                return;

            }

           



        }
    }
}