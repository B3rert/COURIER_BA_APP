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
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//DocumentoCourierPage

namespace CourierBA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GuiaReferenciaPage : ContentPage
    {

        #region Variables Globales
        public GuiaReferenciaViewModel ViewModel { get; set; }
        private List<Tracking> trackings;
        private List<string> listasT = new List<string>();
        private List<Tracking> listasTObject = new List<Tracking>();
        private List<string> listaByte = new List<string>();
        private List<PA_bsc_Moneda_2Model> monedaModels;
        private List<PA_tbl_ReferenciaGuiaModel> referenciaGuiaModels;
        private List<string> nameImageList = new List<string>();
        private List<ImageSource> imageList = new List<ImageSource>();
        int? _Empresa;
        string _NameUSer;
        int _TipoProducto = 90;
        int _SelectMoneda;
        string trackingsList = null;
        int referenciaPadre = 0;
        string messege = null;

        #endregion


        public GuiaReferenciaPage(int? _empresa, string nameUer)
        {
            
            InitializeComponent();
            cargarDatos(_empresa);
            _Empresa = _empresa;
            _NameUSer = nameUer;
            
        }

        /*
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ViewModel = new GuiaReferenciaViewModel();
            await ViewModel.LoadProductos();
            this.BindingContext = ViewModel;

        }
        */

        #region Cargar Monedas
        private async  void cargarDatos(int? empresa)
        {

            UserDialogs.Instance.ShowLoading(title: "Cargando...");

           

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

           
            UserDialogs.Instance.HideLoading();
        }
        #endregion

        #region Tipo producto seleccionado
        /*
        private void SfAutoComplete_SelectionChanged(object sender, Syncfusion.SfAutoComplete.XForms.SelectionChangedEventArgs e)
        {
            var Producto = e.Value as ProductoUso;
            _TipoProducto = Producto.Producto;
            //  await DisplayAlert("",Producto.Descripcion,"OK");
        }
        */
        #endregion

        #region Escaner de codigos qr barra

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

        #endregion

        #region Adjuntar archivos

        private byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private string ByteArrayToString(byte[] arrayString)
        {
            StringBuilder cadena = new StringBuilder(arrayString.Length * 2);
            foreach (byte b in arrayString)
            {
                cadena.AppendFormat("{0:x2}", b);

            }
            return cadena.ToString();
        }

        private Stream ToStream(byte[] bytes)
        {
            return new MemoryStream(bytes)
            {
                Position = 0
            };
        }


        private async void btnSelectFile_Clicked(object sender, EventArgs e)
        {
            collectionImages.ItemsSource = null;

            if (nameImageList.Count != 0)
            {
                nameImageList.Clear();
                imageList.Clear();
                listaByte.Clear();

            }


            var pickResult = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Pick an image"
                
                
            });

            if (pickResult != null)
            {

                var stream = await pickResult.OpenReadAsync();
                var _stream = await pickResult.OpenReadAsync();
                imageList.Add(ImageSource.FromStream(() => stream));
                collectionImages.ItemsSource = imageList;

                var nameImage = pickResult.FileName;
                nameImageList.Add(nameImage);

               streamImage(_stream);



                //lblNameFileSelect.Text = "Archivos selecionados: " + pickResult.Count().ToString();
                lblNameFileSelect.Text = "Archivos selecionados: 1";
            }
        }
        #endregion

        private void streamImage(Stream stream)
        {
            var imgByte = ReadFully(stream);
            var imgByteString = ByteArrayToString(imgByte);

            listaByte.Add(imgByteString);
        }

        #region Tomar fotos
        private async void btnTomarFoto_Clicked(object sender, EventArgs e)
        {
            collectionImages.ItemsSource = null;

            if (nameImageList.Count != 0)
            {
                nameImageList.Clear();
                imageList.Clear();
                listaByte.Clear();

            }
            var cameraOptions = new StoreCameraMediaOptions();
                cameraOptions.PhotoSize = PhotoSize.Medium;
                cameraOptions.SaveToAlbum = true;
                var photo =
                    await Plugin.Media.CrossMedia.Current
                          .TakePhotoAsync(cameraOptions);

                if (photo != null)
                {

                  //  var imageList = new List<ImageSource>();
                    imageList.Add(ImageSource.FromStream(() => { return photo.GetStream(); }));

                    string[] root;
                    root = photo.AlbumPath.Split('/');

                    var nameImage = root[root.Length - 1];

                    nameImageList.Add(nameImage);

                    var imgByte = ReadFully(photo.GetStream());
                    var imgByteString = ByteArrayToString(imgByte);

                    listaByte.Add(imgByteString);

                     collectionImages.ItemsSource = imageList;
                    lblNameFileSelect.Text = "Archivos selecionados: 1";

                    //  cameraImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                    //lblNameFileSelect.Text = "Archivos selecionados: 1";

                }
            
        }
        #endregion

        #region Borrar Imagenes seleccionadas
        private void btnClearImage_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading(title: "Cargando...");
            nameImageList.Clear();
            imageList.Clear();
            listaByte.Clear();
            lblNameFileSelect.Text = "No se elegió ningun archivo";
            collectionImages.ItemsSource = null;
            UserDialogs.Instance.HideLoading();

        }
        #endregion

        private async void btnAgreagar_Clicked(object sender, EventArgs e)
        {
           

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                await DisplayAlert("","Campo Id Tracking  obligatorio","Aceptar");
                txtCodigo.Focus();
                return;
            }
            if(string.IsNullOrEmpty(txtObservacion.Text))
            {
                await DisplayAlert("", "Campo Observación obligatorio", "Aceptar");
                txtObservacion.Focus();
                return;
            }
            /*
            if (string.IsNullOrEmpty(txtProducto.Text))
            {
                await DisplayAlert("", "Campo Tipo producto obligatorio", "Aceptar");
                txtProducto.Focus();
                return;
            }
            */
            if (imageList.Count == 0)
            {
                await DisplayAlert("", "No hay ninguna imagen adjunta", "Aceptar");
                return;
            }

            if (referenciaPadre != 0)
            {

                try
                {

                    HttpClient client = new HttpClient();
                    client.BaseAddress = Global.GlobalVariables.Servidor;

                    string urlvalidarReferencia = string.Format($"/api/PA_Validar_Referencia?" +
                        $"user={_NameUSer}" +
                        $"&descripcion={txtCodigo.Text}" +
                        $"&referencia={referenciaPadre}");

                    var responseValidarReferencia = await client.GetAsync(urlvalidarReferencia);
                    var resultValidarReferencia = responseValidarReferencia.Content.ReadAsStringAsync().Result;

                    if (resultValidarReferencia == "1")
                    {
                        messege = null;
                    }
                    else if (resultValidarReferencia == "0")
                    {
                        messege = "YA EL TRACKING QUE ESTA INTENTANDO INGRESAR YA EXISTE EN ESTA MISMA GUIA";
                    }
                    else
                    {
                        PA_Validar_Referencia myDeserializedClassTracking =
                       JsonConvert.DeserializeObject<PA_Validar_Referencia>(resultValidarReferencia);


                        var tableStringTracking = JsonConvert.SerializeObject(myDeserializedClassTracking.Table);

                        var ValidarMessege = new List<PA_Validar_ReferenciaModel>();

                        ValidarMessege = JsonConvert.DeserializeObject<List<PA_Validar_ReferenciaModel>>(tableStringTracking);


                        messege = ValidarMessege[0].Mensaje;

                    }

                }
                catch (Exception)
                {
                    await DisplayAlert("Error", "No se ha podido conectar con el servidor", "Aceptar");
                    return;
                }

            }

            if (!string.IsNullOrEmpty(messege))
            {
                await DisplayAlert("", messege, "Aceptar");
                return;
            }


            UserDialogs.Instance.ShowLoading(title: "Creando Tracking...");


            //Falta validar una sola guia por tracking
            
            if (listasT.Count == 0)
            {
                //await DisplayAlert("", "Se crea una guia", "ok");
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

                   

                    foreach (var item in referenciaGuiaModels)
                    {
                        referenciaPadre = item.Referencia;
                    }

                    try
                    {

                        string urlReferenciaTracking = string.Format($"/api/PA_tbl_Referencia2?empresa={_Empresa}" +
                            $"&descripcion={txtCodigo.Text}&referenciPadre={referenciaPadre}" +
                            $"&observacion={txtObservacion.Text}&userName={_NameUSer}" +
                            $"&monto={txtMonto.Text}&peso={0}" +
                            $"&pieza={txtPieza.Text}&producto={_TipoProducto}&moneda={_SelectMoneda}");

                        var responseReferenciaTracking = await client.GetAsync(urlReferenciaTracking);
                        var resultTrackingReferencia = responseReferenciaTracking.Content.ReadAsStringAsync().Result;

                        Models.Trackings myDeserializedClassTracking =
                            JsonConvert.DeserializeObject<Models.Trackings>(resultTrackingReferencia);
                        var tableStringTracking = JsonConvert.SerializeObject(myDeserializedClassTracking.Table);

                        trackings = JsonConvert.DeserializeObject<List<Tracking>>(tableStringTracking);

                        foreach (var item in trackings)
                        {
                           // trackingsList = item.Descripcion;

                            var detalles = new Tracking()
                            {
                                Referencia = item.Referencia,
                                Empresa = item.Empresa,
                                Descripcion = item.Descripcion,
                                Referencia_Id = item.Referencia_Id,
                                Raiz = item.Raiz,
                                Nivel = item.Nivel,
                                Referencia_Padre = item.Referencia_Padre,
                                Observacion = item.Observacion,
                                Pais = item.Pais,
                                Fecha_Hora = item.Fecha_Hora,
                                UserName = item.UserName,
                                Fecha_Ini = item.Fecha_Ini,
                                Fecha_Fin = item.Fecha_Fin,
                                Tipo_Referencia = item.Tipo_Referencia,
                                M_Fecha_Hora = item.M_Fecha_Hora,
                                M_UserName = item.M_UserName,
                                Estado = item.Estado,
                                Fecha_Evento = item.Fecha_Evento,
                                Importacion = item.Importacion,
                                Cuenta_Correntista = item.Cuenta_Correntista,
                                Monto_1 = item.Monto_1,
                                Monto_2 = item.Monto_2,
                                Caja_Chica = item.Caja_Chica,
                                Cargo = item.Cargo,
                                Abono = item.Abono,
                                Nom_Pais = item.Nom_Pais,
                                Nom_Cuenta_Correntista = item.Nom_Cuenta_Correntista,
                                HAWB_HBL = item.HAWB_HBL,
                                Suplidor = item.Suplidor,
                                Consignatario = item.Consignatario,
                                Peso = item.Peso,
                                Volumen = item.Volumen,
                                Contenedor = item.Contenedor,
                                Pieza = item.Pieza,
                                Tipo_Pieza = item.Tipo_Pieza,
                                Contenido = item.Contenido,
                                MAWB = item.MAWB,
                                Pais_Destino = item.Pais_Destino,
                                Cuenta_Correntista_Origen = item.Cuenta_Correntista_Origen,
                                Id_Documento_Origen = item.Id_Documento_Origen,
                                Recepcion_Nombre = item.Recepcion_Nombre,
                                Recepcion_Id = item.Recepcion_Id,
                                Tipo_Pago = item.Tipo_Pago,
                                Cantidad = item.Cantidad,
                                Producto = item.Producto,
                                Unidad_Medida = item.Unidad_Medida,
                                Observacion_2 = item.Observacion_2,
                                Observacion_3 = item.Observacion_3,
                                TEU = item.TEU,
                                Elemento_Asignado = item.Elemento_Asignado,
                                Banco = item.Banco,
                                Monto_Cuota = item.Monto_Cuota,
                                Monto_Financiar = item.Monto_Financiar,
                                Monto_Enganche = item.Monto_Enganche,
                                Id_Cuenta = item.Id_Cuenta,
                                Actividad = item.Actividad,
                                Categoria = item.Categoria,
                                SubCategoria = item.SubCategoria,
                                Ref_Serie = item.Ref_Serie,
                                Hotel_Estado = item.Hotel_Estado,
                                Permitir_CxC = item.Permitir_CxC,
                                Orden = item.Orden,
                                Moneda = item.Moneda,
                                Cuenta_Cta = item.Cuenta_Cta,
                                Bodega_Ubicacion = item.Bodega_Ubicacion,
                                Opc_Detalle = item.Opc_Detalle,
                                Referencia_Id_1 = item.Referencia_Id_1,
                            };

                            listasTObject.Add(detalles);
                            //  listaTrackings.Add(detailes);
                        }

                    }
                    catch (Exception er)
                    {
                        var test = er.Message;
                        UserDialogs.Instance.HideLoading();
                        await DisplayAlert("Error", "No se ha podido conectar con el servidor", "Aceptar");
                        return;
                    }

                  
                }
                catch (Exception)
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Error", "No se ha podido conectar con el servidor", "Aceptar");
                    return;
                }

            }
            else
            {
               

                //await DisplayAlert("", "No se crea otra guia", "ok");
                try
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = Global.GlobalVariables.Servidor;

                    string urlReferenciaTracking = string.Format($"/api/PA_tbl_Referencia2?empresa={_Empresa}" +
                        $"&descripcion={txtCodigo.Text}&referenciPadre={referenciaPadre}" +
                        $"&observacion={txtObservacion.Text}&userName={_NameUSer}" +
                        $"&monto={txtMonto.Text}&peso={0}" +
                        $"&pieza={txtPieza.Text}&producto={_TipoProducto}&moneda={_SelectMoneda}");

                    var responseReferenciaTracking = await client.GetAsync(urlReferenciaTracking);
                    var resultTrackingReferencia = responseReferenciaTracking.Content.ReadAsStringAsync().Result;

                    Models.Trackings myDeserializedClassTracking =
                        JsonConvert.DeserializeObject<Models.Trackings>(resultTrackingReferencia);
                    var tableStringTracking = JsonConvert.SerializeObject(myDeserializedClassTracking.Table);

                    trackings = JsonConvert.DeserializeObject<List<Tracking>>(tableStringTracking);

                    foreach (var item in trackings)
                    {
                        // trackingsList = item.Descripcion;

                        var detalles = new Tracking()
                        {
                            Referencia = item.Referencia,
                            Empresa = item.Empresa,
                            Descripcion = item.Descripcion,
                            Referencia_Id = item.Referencia_Id,
                            Raiz = item.Raiz,
                            Nivel = item.Nivel,
                            Referencia_Padre = item.Referencia_Padre,
                            Observacion = item.Observacion,
                            Pais = item.Pais,
                            Fecha_Hora = item.Fecha_Hora,
                            UserName = item.UserName,
                            Fecha_Ini = item.Fecha_Ini,
                            Fecha_Fin = item.Fecha_Fin,
                            Tipo_Referencia = item.Tipo_Referencia,
                            M_Fecha_Hora = item.M_Fecha_Hora,
                            M_UserName = item.M_UserName,
                            Estado = item.Estado,
                            Fecha_Evento = item.Fecha_Evento,
                            Importacion = item.Importacion,
                            Cuenta_Correntista = item.Cuenta_Correntista,
                            Monto_1 = item.Monto_1,
                            Monto_2 = item.Monto_2,
                            Caja_Chica = item.Caja_Chica,
                            Cargo = item.Cargo,
                            Abono = item.Abono,
                            Nom_Pais = item.Nom_Pais,
                            Nom_Cuenta_Correntista = item.Nom_Cuenta_Correntista,
                            HAWB_HBL = item.HAWB_HBL,
                            Suplidor = item.Suplidor,
                            Consignatario = item.Consignatario,
                            Peso = item.Peso,
                            Volumen = item.Volumen,
                            Contenedor = item.Contenedor,
                            Pieza = item.Pieza,
                            Tipo_Pieza = item.Tipo_Pieza,
                            Contenido = item.Contenido,
                            MAWB = item.MAWB,
                            Pais_Destino = item.Pais_Destino,
                            Cuenta_Correntista_Origen = item.Cuenta_Correntista_Origen,
                            Id_Documento_Origen = item.Id_Documento_Origen,
                            Recepcion_Nombre = item.Recepcion_Nombre,
                            Recepcion_Id = item.Recepcion_Id,
                            Tipo_Pago = item.Tipo_Pago,
                            Cantidad = item.Cantidad,
                            Producto = item.Producto,
                            Unidad_Medida = item.Unidad_Medida,
                            Observacion_2 = item.Observacion_2,
                            Observacion_3 = item.Observacion_3,
                            TEU = item.TEU,
                            Elemento_Asignado = item.Elemento_Asignado,
                            Banco = item.Banco,
                            Monto_Cuota = item.Monto_Cuota,
                            Monto_Financiar = item.Monto_Financiar,
                            Monto_Enganche = item.Monto_Enganche,
                            Id_Cuenta = item.Id_Cuenta,
                            Actividad = item.Actividad,
                            Categoria = item.Categoria,
                            SubCategoria = item.SubCategoria,
                            Ref_Serie = item.Ref_Serie,
                            Hotel_Estado = item.Hotel_Estado,
                            Permitir_CxC = item.Permitir_CxC,
                            Orden = item.Orden,
                            Moneda = item.Moneda,
                            Cuenta_Cta = item.Cuenta_Cta,
                            Bodega_Ubicacion = item.Bodega_Ubicacion,
                            Opc_Detalle = item.Opc_Detalle,
                            Referencia_Id_1 = item.Referencia_Id_1,
                        };

                        listasTObject.Add(detalles);
                        //  listaTrackings.Add(detailes);
                    }

                }
                catch (Exception error)
                {
                    var test = error.Message;
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Error", "No se ha podido conectar con el servidor", "Aceptar");
                    return;
                }


               
            }

            //listasT.Add(trackingsList);
            //await DisplayAlert("", trackingsList, "Ok");
           


            int count = 0;

            if (imageList.Count != 0)
            {
                List<string> newName = new List<string>();

                foreach (var name in nameImageList)
                {
                    DateTime dateTime = DateTime.Now;

                    string[] ext;
                    ext = name.Split('.');

                    string newImgName = $"IMG{count.ToString()}{dateTime.ToString("yyyyMMddHHmm")}.{ext[1]}";
                    newName.Add(newImgName);

                    count++;


                    //api imagen
                    try
                    {

                        HttpClient client = new HttpClient();
                        client.BaseAddress = Global.GlobalVariables.Servidor;

                        string urlInsertImage = string.Format($"api/PA_tbl_Referencia_Objeto?" +
                            $"pReferencia={referenciaPadre}" +
                            $"&pUserName={_NameUSer}" +
                            $"&pObjeto_Nombre={newImgName}" +
                            $"&pObservacion_1={name}");

                        var responseImg = await client.GetAsync(urlInsertImage);
                        var resulltImg = responseImg.Content.ReadAsStringAsync().Result;


                    }
                    catch (Exception)
                    {
                        UserDialogs.Instance.HideLoading();
                        await DisplayAlert("Error", "No se ha podido conectar con el servidor", "Aceptar");
                        return;
                    }

                }


                foreach (var _byte in listaByte)
                {
                    foreach (var _name in newName)
                    {
                        var detail = new UpoloadFileModel()
                        {
                            Name = _name,
                            ArrayByte = _byte
                        };

                        //serializar objeto, los campos que se van a insertar en formato Json
                        var jsonRequest = JsonConvert.SerializeObject(detail);
                        var content = new StringContent(jsonRequest, Encoding.UTF8, "text/json");

                        try
                        {
                            HttpClient client = new HttpClient();
                            client.BaseAddress = Global.GlobalVariables.Servidor;
                            //string url = string.Format("api/UploadFile"); //URL API
                            string url = string.Format("api/UploadImage"); //URL API
                            var response = await client.PostAsync(url, content);
                            var postResult = response.Content.ReadAsStringAsync().Result;

                            UserDialogs.Instance.HideLoading();
                            await DisplayAlert("", "Tracking creado", "Aceptar");
                            collectionTracking.ItemsSource = null;
                            collectionTracking.ItemsSource = listasTObject;

                        }
                        catch
                        {
                            UserDialogs.Instance.HideLoading();
                            await DisplayAlert("Error", "No se ha podido conectar con el servidor", "Aceptar");
                            return;
                        }
                    }
                }

                return;
            }

            UserDialogs.Instance.HideLoading();
          // await DisplayAlert("","Tracking creado con éxito","Aceptar");


        }

        private void pickMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {

            int position = pickMoneda.SelectedIndex;

            _SelectMoneda = monedaModels[position].Moneda;

        }

        private async void collectionTracking_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var tracking = e.Item as Tracking;

            bool answer = await DisplayAlert("", "¿Qué deseas hacer?", "Ver estado", "Eliminar");

            if (answer) 
            {
                await Navigation.PushAsync(new TrackingStatusPage(tracking.Descripcion.ToString()));

            }
            else
            {
                bool _answer = await DisplayAlert("Eliminar Tracking", "¿Estás seguro?", "ACEPTAR", "CANCELAR");

                if (_answer)
                {
                    UserDialogs.Instance.ShowLoading(title: "Eliminando Tracking...");


                    HttpClient client = new HttpClient();
                    client.BaseAddress = Global.GlobalVariables.Servidor;
                    string url = string.Format($"/api/AnularReferencia?" +
                        $"user={_NameUSer}" +
                        $"&descripcion={tracking.Descripcion}" +
                        $"&idReferencia={tracking.Referencia_Id}" +
                        $"&referencia={tracking.Referencia}"); //URL API
                    var response = await client.GetAsync(url);
                    var result = response.Content.ReadAsStringAsync().Result;


                   

                    if (result == "1")
                    {
                        await DisplayAlert("", "Se ha eliminado el tracking", "Aceptar");

                        var listasclone = new List<Tracking>();



                        foreach (var item in listasTObject)
                        {
                            // trackingsList = item.Descripcion;

                            var detalles = new Tracking()
                            {
                                Referencia = item.Referencia,
                                Empresa = item.Empresa,
                                Descripcion = item.Descripcion,
                                Referencia_Id = item.Referencia_Id,
                                Raiz = item.Raiz,
                                Nivel = item.Nivel,
                                Referencia_Padre = item.Referencia_Padre,
                                Observacion = item.Observacion,
                                Pais = item.Pais,
                                Fecha_Hora = item.Fecha_Hora,
                                UserName = item.UserName,
                                Fecha_Ini = item.Fecha_Ini,
                                Fecha_Fin = item.Fecha_Fin,
                                Tipo_Referencia = item.Tipo_Referencia,
                                M_Fecha_Hora = item.M_Fecha_Hora,
                                M_UserName = item.M_UserName,
                                Estado = item.Estado,
                                Fecha_Evento = item.Fecha_Evento,
                                Importacion = item.Importacion,
                                Cuenta_Correntista = item.Cuenta_Correntista,
                                Monto_1 = item.Monto_1,
                                Monto_2 = item.Monto_2,
                                Caja_Chica = item.Caja_Chica,
                                Cargo = item.Cargo,
                                Abono = item.Abono,
                                Nom_Pais = item.Nom_Pais,
                                Nom_Cuenta_Correntista = item.Nom_Cuenta_Correntista,
                                HAWB_HBL = item.HAWB_HBL,
                                Suplidor = item.Suplidor,
                                Consignatario = item.Consignatario,
                                Peso = item.Peso,
                                Volumen = item.Volumen,
                                Contenedor = item.Contenedor,
                                Pieza = item.Pieza,
                                Tipo_Pieza = item.Tipo_Pieza,
                                Contenido = item.Contenido,
                                MAWB = item.MAWB,
                                Pais_Destino = item.Pais_Destino,
                                Cuenta_Correntista_Origen = item.Cuenta_Correntista_Origen,
                                Id_Documento_Origen = item.Id_Documento_Origen,
                                Recepcion_Nombre = item.Recepcion_Nombre,
                                Recepcion_Id = item.Recepcion_Id,
                                Tipo_Pago = item.Tipo_Pago,
                                Cantidad = item.Cantidad,
                                Producto = item.Producto,
                                Unidad_Medida = item.Unidad_Medida,
                                Observacion_2 = item.Observacion_2,
                                Observacion_3 = item.Observacion_3,
                                TEU = item.TEU,
                                Elemento_Asignado = item.Elemento_Asignado,
                                Banco = item.Banco,
                                Monto_Cuota = item.Monto_Cuota,
                                Monto_Financiar = item.Monto_Financiar,
                                Monto_Enganche = item.Monto_Enganche,
                                Id_Cuenta = item.Id_Cuenta,
                                Actividad = item.Actividad,
                                Categoria = item.Categoria,
                                SubCategoria = item.SubCategoria,
                                Ref_Serie = item.Ref_Serie,
                                Hotel_Estado = item.Hotel_Estado,
                                Permitir_CxC = item.Permitir_CxC,
                                Orden = item.Orden,
                                Moneda = item.Moneda,
                                Cuenta_Cta = item.Cuenta_Cta,
                                Bodega_Ubicacion = item.Bodega_Ubicacion,
                                Opc_Detalle = item.Opc_Detalle,
                                Referencia_Id_1 = item.Referencia_Id_1,
                            };

                            listasclone.Add(detalles);
                            //  listaTrackings.Add(detailes);
                        }

                        listasTObject.Clear();

                        foreach (var item in listasclone)
                        {
                            if (item.Descripcion == tracking.Descripcion)
                            {
                                continue;
                            }
                            else
                            {
                                var detalles = new Tracking()
                                {
                                    Referencia = item.Referencia,
                                    Empresa = item.Empresa,
                                    Descripcion = item.Descripcion,
                                    Referencia_Id = item.Referencia_Id,
                                    Raiz = item.Raiz,
                                    Nivel = item.Nivel,
                                    Referencia_Padre = item.Referencia_Padre,
                                    Observacion = item.Observacion,
                                    Pais = item.Pais,
                                    Fecha_Hora = item.Fecha_Hora,
                                    UserName = item.UserName,
                                    Fecha_Ini = item.Fecha_Ini,
                                    Fecha_Fin = item.Fecha_Fin,
                                    Tipo_Referencia = item.Tipo_Referencia,
                                    M_Fecha_Hora = item.M_Fecha_Hora,
                                    M_UserName = item.M_UserName,
                                    Estado = item.Estado,
                                    Fecha_Evento = item.Fecha_Evento,
                                    Importacion = item.Importacion,
                                    Cuenta_Correntista = item.Cuenta_Correntista,
                                    Monto_1 = item.Monto_1,
                                    Monto_2 = item.Monto_2,
                                    Caja_Chica = item.Caja_Chica,
                                    Cargo = item.Cargo,
                                    Abono = item.Abono,
                                    Nom_Pais = item.Nom_Pais,
                                    Nom_Cuenta_Correntista = item.Nom_Cuenta_Correntista,
                                    HAWB_HBL = item.HAWB_HBL,
                                    Suplidor = item.Suplidor,
                                    Consignatario = item.Consignatario,
                                    Peso = item.Peso,
                                    Volumen = item.Volumen,
                                    Contenedor = item.Contenedor,
                                    Pieza = item.Pieza,
                                    Tipo_Pieza = item.Tipo_Pieza,
                                    Contenido = item.Contenido,
                                    MAWB = item.MAWB,
                                    Pais_Destino = item.Pais_Destino,
                                    Cuenta_Correntista_Origen = item.Cuenta_Correntista_Origen,
                                    Id_Documento_Origen = item.Id_Documento_Origen,
                                    Recepcion_Nombre = item.Recepcion_Nombre,
                                    Recepcion_Id = item.Recepcion_Id,
                                    Tipo_Pago = item.Tipo_Pago,
                                    Cantidad = item.Cantidad,
                                    Producto = item.Producto,
                                    Unidad_Medida = item.Unidad_Medida,
                                    Observacion_2 = item.Observacion_2,
                                    Observacion_3 = item.Observacion_3,
                                    TEU = item.TEU,
                                    Elemento_Asignado = item.Elemento_Asignado,
                                    Banco = item.Banco,
                                    Monto_Cuota = item.Monto_Cuota,
                                    Monto_Financiar = item.Monto_Financiar,
                                    Monto_Enganche = item.Monto_Enganche,
                                    Id_Cuenta = item.Id_Cuenta,
                                    Actividad = item.Actividad,
                                    Categoria = item.Categoria,
                                    SubCategoria = item.SubCategoria,
                                    Ref_Serie = item.Ref_Serie,
                                    Hotel_Estado = item.Hotel_Estado,
                                    Permitir_CxC = item.Permitir_CxC,
                                    Orden = item.Orden,
                                    Moneda = item.Moneda,
                                    Cuenta_Cta = item.Cuenta_Cta,
                                    Bodega_Ubicacion = item.Bodega_Ubicacion,
                                    Opc_Detalle = item.Opc_Detalle,
                                    Referencia_Id_1 = item.Referencia_Id_1,
                                };

                                listasTObject.Add(detalles);
                            }

                            
                        }



                        collectionTracking.ItemsSource = null;
                        collectionTracking.ItemsSource = listasTObject;
                        UserDialogs.Instance.HideLoading();


                    }
                    else if (result == "0")
                    {
                        await DisplayAlert("", "No se ha podido eliminado el tracking", "Aceptar");

                    }
                    else
                    {
                        await DisplayAlert("", "Ha ocurrido un error", "Aceptar");

                    }



                   
                }



        }

        }
    }
}