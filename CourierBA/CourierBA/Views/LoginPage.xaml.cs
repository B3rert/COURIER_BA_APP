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

namespace CourierBA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void LoginBtn_Clicked(object sender, EventArgs e)
        {

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

            //Consumo API REST
            string result = null;
            string resultEmpresa = null;
            string resultEstacion = null;

            try
            {
                //API usuario
                UserDialogs.Instance.ShowLoading(title: "Cargando...");

                HttpClient client = new HttpClient();
                client.BaseAddress = Global.GlobalVariables.Servidor;
                string url = string.Format("/api/PA_bsc_User_2?user=" + UserEntry.Text + "&pass=" + PassEntry.Text); //URL API
                var response = await client.GetAsync(url);
                result = response.Content.ReadAsStringAsync().Result;

                try
                {
                    //Api Empresa

                    string urlEmpresa = string.Format("/api/PA_bsc_Empresa_1?user=" + UserEntry.Text);
                    var responseEmpresa = await client.GetAsync(urlEmpresa);
                    resultEmpresa = responseEmpresa.Content.ReadAsStringAsync().Result;

                    try
                    {
                        //Api estacion 
                        string urlEstacion = string.Format("/api/PA_bsc_Estacion_Trabajo_2?user=" + UserEntry.Text);
                        var responseEstacion = await client.GetAsync(urlEstacion);
                        resultEstacion = responseEstacion.Content.ReadAsStringAsync().Result;

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
            if (string.IsNullOrEmpty(result) || result == "0")
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Error", "Usuario y/o contraseña incorrecta", "Aceptar");
                return;
            }
            else
            {
                UserDialogs.Instance.HideLoading();
                await Navigation.PushModalAsync(new LocalConfigPage(resultEmpresa, resultEstacion, UserEntry.Text));
                PassEntry.Text = string.Empty;
            }

        }
    }
}