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
    public partial class PerflPage : ContentPage
    {
        public PerflPage()
        {
            InitializeComponent();
        }

      

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {

            

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                await DisplayAlert("", "No pueden haber campos vacios", "Aceptar");
                return;

            }
            else if (string.IsNullOrEmpty(txtDireccionEntrega.Text))
            {
                await DisplayAlert("", "No pueden haber campos vacios", "Aceptar");
                return;


            }
            else if (string.IsNullOrEmpty(txtDireccionOrigen.Text))
            {

                await DisplayAlert("", "No pueden haber campos vacios", "Aceptar");
                return;

            }
            else if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                await DisplayAlert("", "No pueden haber campos vacios", "Aceptar");
                return;

            }
            else if (string.IsNullOrEmpty(txtClaveActual.Text))
            {
                await DisplayAlert("", "No pueden haber campos vacios", "Aceptar");
                return;

            }
            else if (string.IsNullOrEmpty(txtNuevaClave.Text))
            {
                await DisplayAlert("", "No pueden haber campos vacios", "Aceptar");
                return;

            }
            else if (string.IsNullOrEmpty(txtNit.Text))
            {
                await DisplayAlert("", "No pueden haber campos vacios", "Aceptar");
                return;

            }
            else if (string.IsNullOrEmpty(txtNombre2.Text))
            {
                await DisplayAlert("", "No pueden haber campos vacios", "Aceptar");
                return;

            }
            else if (string.IsNullOrEmpty(txtAsesor.Text))
            {
                await DisplayAlert("", "No pueden haber campos vacios", "Aceptar");
                return;
            }
            else if (txtNuevaClave.Text.Length != 7)
            {
                await DisplayAlert("", "La nuvea clave debe contener al menos 7 caracteres", "Aceptar");
                return;
            }

        }
    }
}