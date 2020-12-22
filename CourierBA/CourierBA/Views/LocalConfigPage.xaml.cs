using Acr.UserDialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CourierBA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocalConfigPage : ContentPage
    {

        private List<Models.EmpresaModel> empresaModels;
        private List<Models.EstacionModel> EstacionModels;
        int selectedEmpresa;
        int selectedEstacion;
        string _usuario;

        public LocalConfigPage(string Empresa, string Estacion, string Usuario)
        {
            InitializeComponent();

            _usuario = Usuario;
            //Empresa;
            Models.Empresa myDeserializedClassEmpresa = JsonConvert.DeserializeObject<Models.Empresa>(Empresa);
            var tableString = JsonConvert.SerializeObject(myDeserializedClassEmpresa.Table);
            empresaModels = JsonConvert.DeserializeObject<List<Models.EmpresaModel>>(tableString);
            registrosLbl.Text = empresaModels.Count.ToString();
            EmpresaList.ItemsSource = empresaModels;

            //Estacion;
            Models.Estacion myDeserializedClassEstacion = JsonConvert.DeserializeObject<Models.Estacion>(Estacion);
            var _tableString = JsonConvert.SerializeObject(myDeserializedClassEstacion.Table);
            EstacionModels = JsonConvert.DeserializeObject<List<Models.EstacionModel>>(_tableString);
            registrosLbl2.Text = EstacionModels.Count.ToString();
            EstacionList.ItemsSource = EstacionModels;
        }

        private void EmpresaList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var empresa = e.Item as Models.EmpresaModel;
            selectedEmpresa = empresa.Empresa.Value;
            return;
        }

        private void EstacionList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var estacion = e.Item as Models.EstacionModel;
            selectedEstacion = estacion.Estacion_Trabajo.Value;
            return;
        }

        private async void saveConfigLocal_btn(object sender, EventArgs e)
        {
            //Validar conexion a internet
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Error", "No se ha detectado una conexion a internet", "Aceptar");
                return;
            }

            string result = null;
            try
            {
                UserDialogs.Instance.ShowLoading(title: "Cargando...");

                HttpClient client = new HttpClient();
                client.BaseAddress = Global.GlobalVariables.Servidor;
                string url = string.Format("api/AplicationUser?user=" + _usuario); //URL API
                var response = await client.GetAsync(url);
                result = response.Content.ReadAsStringAsync().Result;

            }
            catch
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Error", "No se ha podido conectar con el servidor", "Aceptar");
                return;
            }

            UserDialogs.Instance.HideLoading();
            await Navigation.PushModalAsync(new MenuDetailPage(result, _usuario));

        }
    }
}