using Acr.UserDialogs;
using CourierBA.Helpers;
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
    public partial class ResetPasswordPage : ContentPage
    {
        public ResetPasswordPage()
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
            bool answer = await DisplayAlert("Cancelar", "¿Estás seguro?", "ACEPTAR", "CANCELAR");

            if (answer)
                await Navigation.PopToRootAsync();
        }
        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
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

            UserDialogs.Instance.ShowLoading(title: "Restaurando contraseña");



            HttpClient client = new HttpClient();
            client.BaseAddress = Global.GlobalVariables.Servidor;
            string url = string.Format($"/api/PA_Recuperar_User?" +
                $"Correo={txtCorreo.Text}"); //URL API
            var response = await client.GetAsync(url);
            var result = response.Content.ReadAsStringAsync().Result;


            UserDialogs.Instance.HideLoading();

            if (result == "1")
            {
                await DisplayAlert("Contraseña restaurada", "Se ha enviado un correo con la informacion del usuario proporcionado.", "Aceptar");
                await Navigation.PopToRootAsync();
            }
            else if (result == "0")
            {
                await DisplayAlert("", "Este usuario no se encuentra registrado.", "Aceptar");

            }
            else
            {
                await DisplayAlert("", "Erro de servidor", "Aceptar");

            }
        }
    }
}