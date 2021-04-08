using CourierBA.Models;
using CourierBA.ViewModels;
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
    public partial class ListTrackingUserPage : ContentPage
    {
        ListTrackingUserViewModel listTrackingUserViewModel;
        public ListTrackingUserPage()
        {
            InitializeComponent();
            listTrackingUserViewModel = new ListTrackingUserViewModel();
            BindingContext = listTrackingUserViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ListaElemntos elemntos = new ListaElemntos();

            list1.ItemsSource = elemntos._elementos;
        }
    
    }
}