using Acr.UserDialogs;
using CourierBA.Helpers;
using CourierBA.Models;
using CourierBA.Services;
using CourierBA.ViewModels;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class GuiaReferenciaPage : ContentPage
    {
        public GuiaReferenciaViewModel ViewModel { get; set; }
        public ObservableCollection<Tracking> TrackingModels { get; set; }

        private List<PA_bsc_Moneda_2Model> monedaModels;
        private List<PA_tbl_ReferenciaGuiaModel> referenciaGuiaModels;
        int? _Empresa;
        string _NameUSer;
        int _TipoProducto = 0;
        int _SelectMoneda;

        public GuiaReferenciaPage(int? _empresa, string nameUer)
        {
            InitializeComponent();
            cargarDatos(_empresa);
            _Empresa = _empresa;
            _NameUSer = nameUer;
            
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
                pickMoneda.SelectedIndex = 0;
                _SelectMoneda = monedaModels[0].Moneda;
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

        private  void SfAutoComplete_SelectionChanged(object sender, Syncfusion.SfAutoComplete.XForms.SelectionChangedEventArgs e)
        {
            var Producto = e.Value as ProductoUso;
            _TipoProducto = Producto.Producto;
            //  await DisplayAlert("",Producto.Descripcion,"OK");
        }

        private async void btnEscanearCodigo_Clicked(object sender, EventArgs e)
        {
            try
            {
                var scanner = DependencyService.Get<IQrScanningService>();
                var result = await scanner.ScanAsync();
                if (result != null)
                {
                    txtCodigo.Text = result;
                }
                else
                {
                    await DisplayAlert("404", "No se han encontrados datos", "Aceptar");
                    return;
                }
            }
            catch
            {
                await DisplayAlert("Error", "Error al escanear", "Aceptar");
                return;
            }
        }

        private async void btnSelectFile_Clicked(object sender, EventArgs e)
        {
            var pickResult = await FilePicker.PickMultipleAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Pick an image(s)"
            });

            if (pickResult != null)
            {
                collectionImages.ItemsSource = null;
                var imageList = new List<ImageSource>();
                foreach (var image in pickResult)
                {
                    var stream = await image.OpenReadAsync();
                    imageList.Add(ImageSource.FromStream(() => stream));

                }
                collectionImages.ItemsSource = imageList;
                lblNameFileSelect.Text = "Archivos selecionados: " + pickResult.Count().ToString();
            }
        }

        private async void btnTomarFoto_Clicked(object sender, EventArgs e)
        {
            var cameraOptions = new StoreCameraMediaOptions();
            cameraOptions.PhotoSize = PhotoSize.Medium;
            cameraOptions.SaveToAlbum = true;
            var photo =
                await Plugin.Media.CrossMedia.Current
                      .TakePhotoAsync(cameraOptions);



            if (photo != null)
            {
                collectionImages.ItemsSource = null;
                var imageList = new List<ImageSource>();
                imageList.Add(ImageSource.FromStream(() => { return photo.GetStream(); }));


                collectionImages.ItemsSource = imageList;
                lblNameFileSelect.Text = "Archivos selecionados: 1";

                //  cameraImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                //lblNameFileSelect.Text = "Archivos selecionados: 1";

            }

        }

        private void btnClearImage_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading(title: "Cargando...");
            lblNameFileSelect.Text = "No se elegió ningun archivo";
            collectionImages.ItemsSource = null;
            UserDialogs.Instance.HideLoading();

        }

        private async void btnAgreagar_Clicked(object sender, EventArgs e)
        {
           

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                await DisplayAlert("","Campo Id Tracking  obligatorio","Aceptar");
                return;
            }
            if(string.IsNullOrEmpty(txtObservacion.Text))
            {
                await DisplayAlert("", "Campo Observación obligatorio", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(txtProducto.Text))
            {
                await DisplayAlert("", "Campo Tipo producto obligatorio", "Aceptar");
                return;
            }


            UserDialogs.Instance.ShowLoading(title: "Cargando...");

            try
            {

                HttpClient client = new HttpClient();
                client.BaseAddress = Global.GlobalVariables.Servidor;
                string urlReferenciaGuia = string.Format($"/api/PA_tbl_Referencia?empresa={_Empresa}&userName={_NameUSer}");
                var responseReferenciaGuia = await client.GetAsync(urlReferenciaGuia);
                var resultGuiaReferencia = responseReferenciaGuia.Content.ReadAsStringAsync().Result;

                Models.PA_tbl_ReferenciaGuia myDeserializedClassGuia =
                    JsonConvert.DeserializeObject<Models.PA_tbl_ReferenciaGuia>(resultGuiaReferencia);
                var tableString = JsonConvert.SerializeObject(myDeserializedClassGuia.Table);

                referenciaGuiaModels = JsonConvert.DeserializeObject<List<PA_tbl_ReferenciaGuiaModel>>(tableString);

                int referenciaPadre = 0;

                foreach (var item in referenciaGuiaModels)
                {
                    referenciaPadre = item.Referencia;
                }

                try
                {
                    var url = $"/api/PA_tbl_Referencia2?empresa={_Empresa}" +
                        $"&descripcion={txtCodigo.Text}&referenciPadre={referenciaPadre}" +
                        $"&observacion={txtObservacion.Text}&userName={_NameUSer}" +
                        $"&monto={txtMonto.Text}&peso={txtPeso.Text}" +
                        $"&pieza={txtPieza.Text}&producto={_TipoProducto}&moneda={_SelectMoneda}";
                    var service =
                        new HttpHelper<Trackings>();
                    var tracking = await service.GetRestServiceDataAsync(url);

                    TrackingModels = 
                        new ObservableCollection<Tracking>(tracking.Table);

                    collectionTracking.ItemsSource = TrackingModels;
                }
                catch (Exception)
                {

                    throw;
                }

                UserDialogs.Instance.HideLoading();
            }
            catch (Exception)
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Error", "No se ha podido conectar con el servidor", "Aceptar");
                return;
            }

        }

        private void pickMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {

            int position = pickMoneda.SelectedIndex;

            _SelectMoneda = monedaModels[position].Moneda;

        }
    }
}