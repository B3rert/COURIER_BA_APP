using CourierBA.Helpers;
using CourierBA.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CourierBA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestDbLocalPage : ContentPage
    {
        public ObservableCollection<ProductoUso> ProductoUsos { get; set; }

        public TestDbLocalPage()
        {
            InitializeComponent();
        }
        public async Task LoadProductos()
        {
            //IsBusy = true;

            var url = "/api/PA_bsc_Producto_Uso_2";
            var service =
                new HttpHelper<ProductosUso>();
            var productos = await service.GetRestServiceDataAsync(url);

            ProductoUsos = new ObservableCollection<ProductoUso>(productos.Table);

           // IsBusy = false;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadProductos();
            collectionView.ItemsSource = await App.Database.GetDbProductos();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            foreach (var item in ProductoUsos)
            {
                await App.Database.SaveDbProductos(new ProductoUso
                {
                    Producto = item.Producto,
                    Descripcion = item.Descripcion,
                    Producto_Uso = item.Producto_Uso
                });
                
                collectionView.ItemsSource = await App.Database.GetDbProductos();

            }
        }
    }
}