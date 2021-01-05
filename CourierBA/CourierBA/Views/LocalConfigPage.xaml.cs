using Acr.UserDialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CourierBA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocalConfigPage : ContentPage
    {

        #region Variables globales 

        private List<Models.EmpresaModel> empresaModels;
        private List<Models.EstacionModel> EstacionModels;
        private List<Models.PA_tbl_UserModel> UserModels;
        int? selectedEmpresa;
        int? countEmpresa;
        int? countEstacion;
        int? selectedEstacion;
        string _usuario;

        #endregion

        public LocalConfigPage(string Empresa, string Estacion, string Usuario, string dtUser)
        {
            InitializeComponent();

            #region Carga inical de datos

            _usuario = Usuario;
            //Empresa;
            Models.Empresa myDeserializedClassEmpresa = JsonConvert.DeserializeObject<Models.Empresa>(Empresa);
            var tableString = JsonConvert.SerializeObject(myDeserializedClassEmpresa.Table);
            empresaModels = JsonConvert.DeserializeObject<List<Models.EmpresaModel>>(tableString);
            registrosLbl.Text = empresaModels.Count.ToString();
            countEmpresa = empresaModels.Count;
            EmpresaList.ItemsSource = empresaModels;

            //Estacion;
            Models.Estacion myDeserializedClassEstacion = JsonConvert.DeserializeObject<Models.Estacion>(Estacion);
            var _tableString = JsonConvert.SerializeObject(myDeserializedClassEstacion.Table);
            EstacionModels = JsonConvert.DeserializeObject<List<Models.EstacionModel>>(_tableString);
            registrosLbl2.Text = EstacionModels.Count.ToString();
            countEstacion = EstacionModels.Count;
            EstacionList.ItemsSource = EstacionModels;

            //DataUser
            Models.PA_tbl_User myDeserializedClassDtUser = JsonConvert.DeserializeObject<Models.PA_tbl_User>(dtUser);
            var _table_string = JsonConvert.SerializeObject(myDeserializedClassDtUser.Table);
            UserModels = JsonConvert.DeserializeObject<List<Models.PA_tbl_UserModel>>(_table_string);

            #endregion

        }


        #region Eventos de seleccion configuracion local

        //Empresa seleccionada
        private void EmpresaList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var empresa = e.Item as Models.EmpresaModel;
            selectedEmpresa = empresa.Empresa.Value;
            return;
        }

        //Estacion seleccionada
        private void EstacionList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var estacion = e.Item as Models.EstacionModel;
            selectedEstacion = estacion.Estacion_Trabajo.Value;
            return;
        }

        #endregion


        //Guardar configuracion local
        private async void saveConfigLocal_btn(object sender, EventArgs e)
        {

            #region Validaciones iniciales

            //Validar conexion a internet
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Error", "No se ha detectado una conexion a internet", "Aceptar");
                return;
            }

            #endregion

            #region Procedimientos de actualizacion en db configuracion local

            if ((selectedEstacion == 0 || selectedEstacion == null) || (selectedEmpresa == null || selectedEmpresa == 0))
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("", "Seleccione una empresa y/o estación de trabajo para continuar.", "Aceptar");
            }
            else
            {
                var insertconfigLocal = new Models.ParamPa_tbl_UserModels();

                foreach (var row in UserModels)
                {
                    insertconfigLocal = new Models.ParamPa_tbl_UserModels()
                    {
                        Accion = 2,
                        Opcion = 0,
                        UserName = row.UserName,
                        Pass_Key = DBNull.Value,
                        Name = row.Name,
                        Description = row.Description,
                        Disable = row.Disable,
                        Language_ID = row.Language_ID,
                        Empresa = selectedEmpresa,
                        Fecha_Server = row.Fecha_Server,
                        Hora_Server = row.Hora_Server,
                        Mod_Fecha = row.Mod_Fecha,
                        Update_Fecha = row.Update_Fecha,
                        Fecha_Usuario = row.Fecha_Usuario,
                        Fecha_Hora = row.Fecha_Hora,
                        M_Fecha_Hora = row.M_Fecha_Hora,
                        M_UserName = row.M_UserName,
                        Periodo = row.Periodo,
                        Fecha_Ini = row.Fecha_Ini,
                        Fecha_Fin = row.Fecha_Fin,
                        Asignar_Rango_Fecha = row.Asignar_Rango_Fecha,
                        Tipo_Nomenclatura = row.Tipo_Nomenclatura,
                        Application = row.Application,
                        Val_Nomenclatura_Contable = row.Val_Nomenclatura_Contable,
                        Pass_Key_Len = row.Pass_Key_Len,
                        Pass_Key_Fecha = row.Pass_Key_Fecha,
                        Pass_Key_Dias = row.Pass_Key_Dias,
                        Val_Serie_Documento = row.Val_Serie_Documento,
                        Ocultar_SAE = row.Ocultar_SAE,
                        Path_Reporte = row.Path_Reporte,
                        Fecha_Ini_Valido = row.Fecha_Ini_Valido,
                        Fecha_Fin_Valido = row.Fecha_Fin_Valido,
                        Val_Rol_Poliza = row.Val_Rol_Poliza,
                        Estacion_Trabajo = selectedEstacion,
                        Permitir_CxC = row.Permitir_CxC,
                        ReportServerUrl = row.ReportServerUrl,
                        Tipo_Entidad = row.Tipo_Entidad,
                        Sexo = row.Sexo,
                        Celular = row.Celular,
                        EMail = row.EMail,
                        SQL_Partida_Contable = row.SQL_Partida_Contable,
                        Cuenta_Correntista = row.Cuenta_Correntista,
                        Val_Poliza = row.Val_Poliza,
                        Val_Elemento_Asignado = row.Val_Elemento_Asignado,
                        Val_Referencia = row.Val_Referencia,
                        Tarea_Nivel = row.Tarea_Nivel,
                        Tipo_Usuario = row.Tipo_Usuario,
                        Val_IP_Address = row.Val_IP_Address,
                        Elemento_Asignado = row.Elemento_Asignado,
                        Tipo_Modelo = row.Tipo_Modelo,
                        PIN = row.PIN,
                        Val_Tipo_Referencia = row.Val_Tipo_Referencia,
                        Val_Id_Dispositivo = row.Val_Id_Dispositivo,
                        Val_Cuenta_Bancaria = row.Val_Cuenta_Bancaria,
                        Val_Localizacion = row.Val_Localizacion,
                        Val_Tipo_Precio = row.Val_Tipo_Precio,
                        Val_Tipo_Accion_Serie_Documento = row.Val_Tipo_Accion_Serie_Documento


                    };
                }

                //serializar objeto, los campos que se van a insertar en formato Json
                var jsonRequest = JsonConvert.SerializeObject(insertconfigLocal);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "text/json");

                try
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = Global.GlobalVariables.Servidor;
                    string url = string.Format("/api/PA_tbl_User"); //URL API
                    var response = await client.PostAsync(url, content);
                    var postResult = response.Content.ReadAsStringAsync().Result;

                    await Navigation.PushModalAsync(new MenuDetailPage(_usuario));
                    UserDialogs.Instance.HideLoading();

                }
                catch
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Error", "No se ha podido conectar con el servidor", "Aceptar");
                    return;
                }
            }

            #endregion

        }
    }
}