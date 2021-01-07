using Acr.UserDialogs;
using CourierBA.Models;
using CourierBA.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CourierBA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GuiaReferenciaPage : ContentPage
    {
        public GuiaReferenciaViewModel ViewModel { get; set; }
        private List<Models.PA_bsc_Moneda_2Model> monedaModels;
        

        public GuiaReferenciaPage(int? _empresa)
        {
            InitializeComponent();
            cargarDatos(_empresa);
        }

        private async  void cargarDatos(int? empresa)
        {

            UserDialogs.Instance.ShowLoading(title: "Cargando...");

            #region Cargar Monedas

            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = Global.GlobalVariables.Servidor;
                string urlMoneda = string.Format($"/api/PA_bsc_Moneda_2?Empresa={empresa}");
                var resMoneda = await client.GetAsync(urlMoneda);
                var resultMoneda = resMoneda.Content.ReadAsStringAsync().Result;

                Models.PA_bsc_Moneda_2 myDeserializedClassMoneda =
                    JsonConvert.DeserializeObject<Models.PA_bsc_Moneda_2>(resultMoneda);

                var tableString = JsonConvert.SerializeObject(myDeserializedClassMoneda.Table);
                monedaModels = JsonConvert.DeserializeObject<List<Models.PA_bsc_Moneda_2Model>>(tableString);

                pickMoneda.ItemsSource = monedaModels;
            }
            catch
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("", "No se han podido cargar algunos datos", "Aceptar");
            }

            #endregion

            UserDialogs.Instance.HideLoading();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ViewModel = new GuiaReferenciaViewModel();
            await ViewModel.LoadProductos();
            this.BindingContext = ViewModel;

        }

        private async void SfAutoComplete_SelectionChanged(object sender, Syncfusion.SfAutoComplete.XForms.SelectionChangedEventArgs e)
        {
            var Producto = e.Value as ProductoUso;

            //  await DisplayAlert("",Producto.Descripcion,"OK");
        }
    }
}