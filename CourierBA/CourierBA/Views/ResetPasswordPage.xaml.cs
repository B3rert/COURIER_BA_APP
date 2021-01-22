using Acr.UserDialogs;
using CourierBA.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
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
            await Task.Delay(3000);
            await DisplayAlert("", "La nueva contraseña ha sido enviada al correo electrónico.", "Aceptar");
            await Navigation.PopToRootAsync();
            UserDialogs.Instance.HideLoading();

        }
    }
}