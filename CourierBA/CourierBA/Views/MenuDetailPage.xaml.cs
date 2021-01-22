using CourierBA.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CourierBA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuDetailPage : MasterDetailPage
    {
        MenuViewModel VM = new MenuViewModel();
        string _user = null;
        int? _empresa = 0;

        public MenuDetailPage( string user, int? empresa)
        {
            _empresa = empresa;
            _user = user;
            InitializeComponent();
            BindingContext = VM;
            home();
        }

      public void NavigateToPage(Page page)
        {
            Detail = new NavigationPage(new Page());
            IsPresented = false;

        }

        private void home()
        {
            Detail = new NavigationPage(new GuiaReferenciaPage(_empresa, _user));
        }

        private async void btnLogout_Clicked(object sender, EventArgs e)
        {
              bool answer = await DisplayAlert("Cerrar sesión", "¿Estás seguro?", "ACEPTAR", "CANCELAR");

           if (answer)
                await Navigation.PopToRootAsync();
        }

       
        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM._user = _user;
            VM.LoadData();
        }
    }
}