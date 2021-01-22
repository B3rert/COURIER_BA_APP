using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Acr.UserDialogs;
using System.Net.Http;
using Newtonsoft.Json;

namespace CourierBA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        #region Variables Golbales

        private List<Models.EmpresaModel> empresaModels;
        private List<Models.EstacionModel> EstacionModels;
        private List<Models.PA_tbl_UserModel> UserModels;
        private List<Models.UserLog> userLogs;
        string User = null;
        int valLog = 0;
        int? countEmpresa;
        int? countEstacion;
    
        #endregion

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void LoginBtn_Clicked(object sender, EventArgs e)
        {
            #region Validaciones iniciales
           
            //Validar Campos vacios
            if (string.IsNullOrEmpty(UserEntry.Text) || string.IsNullOrEmpty(PassEntry.Text))
            {
                await DisplayAlert("Error", "Los campos usuario y/o contraseña no deben estar vacíos", "Aceptar");
                return;
            }

            //Validar conexion a internet
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Error", "No se ha detectado una conexion a internet", "Aceptar");
                return;
            }

            #endregion

            #region Consumo de apis
            //Consumo API REST
            string result = null;
            string resultEmpresa = null;
            string resultEstacion = null;

            UserDialogs.Instance.ShowLoading(title: "Cargando...");
            try
            {
                //API usuario
                

                HttpClient client = new HttpClient();
                client.BaseAddress = Global.GlobalVariables.Servidor;
                string url = string.Format($"/api/PA_bsc_User_2?user={UserEntry.Text}&pass={PassEntry.Text}"); //URL API
                var response = await client.GetAsync(url);
                result = response.Content.ReadAsStringAsync().Result;

                Models.UserLogin userLogin = JsonConvert.DeserializeObject<Models.UserLogin>(result);
                var userTableString = JsonConvert.SerializeObject(userLogin.Table);
                userLogs = JsonConvert.DeserializeObject<List<Models.UserLog>>(userTableString);


                valLog = userLogs[0].valor;
                

                if (valLog == 0)
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Error", "Usuario y/o contraseña incorrecta", "Aceptar");
                    return;
                }
                else
                {
                    User = userLogs[0].UserName.ToString();
                }


                try
                {
                    //Api Empresa
                    string urlEmpresa = string.Format($"/api/PA_bsc_Empresa_1?user={User}");
                    var responseEmpresa = await client.GetAsync(urlEmpresa);
                    resultEmpresa = responseEmpresa.Content.ReadAsStringAsync().Result;

                    Models.Empresa myDeserializedClassEmpresa = JsonConvert.DeserializeObject<Models.Empresa>(resultEmpresa);
                    var tableString = JsonConvert.SerializeObject(myDeserializedClassEmpresa.Table);
                    empresaModels = JsonConvert.DeserializeObject<List<Models.EmpresaModel>>(tableString);
                    countEmpresa = empresaModels.Count;

                    try
                    {
                        //Api estacion 
                        string urlEstacion = string.Format($"/api/PA_bsc_Estacion_Trabajo_2?user={User}");
                        var responseEstacion = await client.GetAsync(urlEstacion);
                        resultEstacion = responseEstacion.Content.ReadAsStringAsync().Result;

                        //Estacion;
                        Models.Estacion myDeserializedClassEstacion = JsonConvert.DeserializeObject<Models.Estacion>(resultEstacion);
                        var _tableString = JsonConvert.SerializeObject(myDeserializedClassEstacion.Table);
                        EstacionModels = JsonConvert.DeserializeObject<List<Models.EstacionModel>>(_tableString);
                        countEstacion = EstacionModels.Count;
                    }
                    catch
                    {
                        UserDialogs.Instance.HideLoading();
                        await DisplayAlert("Error", "No se ha podido conectar con el servidor", "Aceptar");
                        return;
                    }
                }
                catch
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Error", "No se ha podido conectar con el servidor", "Aceptar");
                    return;
                }
            }
            catch
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Error", "No se ha podido conectar con el servidor", "Aceptar");
                return;
            }

            //Validar Resultado APP
            if (string.IsNullOrEmpty(result) || valLog == 0)
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Error", "Usuario y/o contraseña incorrecta", "Aceptar");
                return;
            }
            else
            {
                try
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = Global.GlobalVariables.Servidor;
                    string url = string.Format($"/api/PA_tbl_User?userName={User}"); //URL API
                    var response = await client.GetAsync(url);
                    var resultPA_tbl_User = response.Content.ReadAsStringAsync().Result;

                    Models.PA_tbl_User myDeserializedClassPA_tbl_User = JsonConvert.DeserializeObject<Models.PA_tbl_User>(resultPA_tbl_User);
                    var tableUsertbl = JsonConvert.SerializeObject(myDeserializedClassPA_tbl_User.Table);
                    UserModels = JsonConvert.DeserializeObject<List<Models.PA_tbl_UserModel>>(tableUsertbl);


                    int? selectedEmpresa = null;
                    int? selectedEstacion = null;

                    if (countEstacion == 1 && countEmpresa == 1)
                    {
                        foreach (var row in empresaModels)
                        {
                            selectedEmpresa = row.Empresa;
                        }
                        foreach (var row in EstacionModels)
                        {
                            selectedEstacion = row.Estacion_Trabajo;
                        }

                        //validacion configuracion local, consumo api

                        var insertconfigLocal = new Models.ParamPa_tbl_UserModels();

                        foreach (var row in UserModels)
                        {
                            insertconfigLocal = new Models.ParamPa_tbl_UserModels()
                            {
                                Accion = 2,
                                Opcion = 0,
                                UserName = row.UserName,
                                Pass_Key = row.Pass_Key,
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
                            HttpClient _client = new HttpClient();
                            _client.BaseAddress = Global.GlobalVariables.Servidor;
                            string _url = string.Format("/api/PA_tbl_User"); //URL API
                            var _response = await client.PostAsync(_url, content);
                            var _result = _response.Content.ReadAsStringAsync().Result;

                            if (_result == "201")
                            {
                                UserDialogs.Instance.HideLoading();
                                await Navigation.PushAsync(new MenuDetailPage(User, selectedEmpresa));
                                PassEntry.Text = string.Empty;
                            }
                            else
                            {
                                UserDialogs.Instance.HideLoading();
                                await DisplayAlert("Error", "Usuario y/o contraseña incorrecta", "Aceptar");
                                return;
                            }
                        }
                        catch
                        {
                            UserDialogs.Instance.HideLoading();
                            await DisplayAlert("Error", "Usuario y/o contraseña incorrecta", "Aceptar");
                            return;
                        }
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                        await Navigation.PushAsync(new LocalConfigPage(resultEmpresa, resultEstacion, User, result));
                        PassEntry.Text = string.Empty;
                      //  Navigation.RemovePage(this);
                    }
                }
                catch(Exception err)
                {
                    var test = err.Message;

                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Error", "Usuario y/o contraseña incorrecta", "Aceptar");
                    return;
                }
            }

            #endregion
        }

        private async void RegisterBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserRegisterPage());

            //await DisplayAlert("", "Siguiente página", "Ok");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResetPasswordPage());

        }
    }
}