using Acr.UserDialogs;
using CourierBA.Helpers;
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
    public partial class UserRegisterPage : ContentPage
    {
        public UserRegisterPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {

            cancel();

            //var test = base.OnBackButtonPressed();

            
            return true;
        }

        private async void cancel()
        {
            bool answer = await DisplayAlert("Cancelar Registro", "¿Estás seguro?", "ACEPTAR", "CANCELAR");

            if (answer)
                await Navigation.PopToRootAsync();
        }

        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                await DisplayAlert("", "No se ha ingresado un Nombre", "Aceptar");
                txtNombre.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtApellido.Text))
            {
                await DisplayAlert("", "No se ha ingresado un Apellido", "Aceptar");
                txtApellido.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                await DisplayAlert("", "No se ha ingresado un Télefono", "Aceptar");
                txtTelefono.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtCorreo.Text))
            {
                await DisplayAlert("", "No se ha ingresado un Correo", "Aceptar");
                txtCorreo.Focus();
                return;
            }

            if (!RegexUtilities.IsValidEmail(txtCorreo.Text))
            {
                await DisplayAlert("", "Correo electrónico invalido", "Aceptar");
                txtCorreo.Focus();
                return;
            }

            UserDialogs.Instance.ShowLoading(title: "Creando usuario...");

            HttpClient client = new HttpClient();
            client.BaseAddress = Global.GlobalVariables.Servidor;
            string url = string.Format($"/api/PA_Registro_User?" +
                $"Nombre={txtNombre.Text}" +
                $"&Apellido={txtApellido.Text}" +
                $"&Telefono={txtTelefono.Text}" +
                $"&Correo={txtCorreo.Text}"); //URL API
            var response = await client.GetAsync(url);
            var result = response.Content.ReadAsStringAsync().Result;


            UserDialogs.Instance.HideLoading();

            if (result == "1")
            {
                await DisplayAlert("Usuario creado", "Se ha enviado un correo electronico al correo proporcionado con sus credenciales.", "Aceptar");
                await Navigation.PopToRootAsync();
            }
            else if (result == "0")
            {
                await DisplayAlert("", "Alerta! Este correo ya existe registrado, intenta recuperar tu contraseña.", "Aceptar");

            }
            else
            {
                await DisplayAlert("", "Erro de servidor", "Aceptar");

            }


        }
    }
}