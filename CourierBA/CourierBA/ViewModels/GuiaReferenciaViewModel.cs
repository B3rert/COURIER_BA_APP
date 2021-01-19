using CourierBA.Helpers;
using CourierBA.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CourierBA.ViewModels
{
    public class GuiaReferenciaViewModel: BaseViewModel
    {
        public ObservableCollection<ProductoUso> ProductoUsos { get; set; }
       // public ObservableCollection<DbProductos> ProductoUsosDb { get; set; }
        public ICommand SerachCommand { get; set; }

        public GuiaReferenciaViewModel()
        {
           /*
            SerachCommand =
                new Command(async (Text) =>
                {
                    //la variable text tiene el valor agregado en serachBar
                });
           */
        }

        public async Task LoadProductos()
        {
            if (Application.Current.Properties.ContainsKey("Datos"))
            {
                IsBusy = true;

                var val = Convert.ToInt32(Application.Current.Properties["Datos"]);
             
                ProductoUsos = new ObservableCollection<ProductoUso>(await App.Database.GetDbProductos());
                
                IsBusy = false;

            }
            else
            {
                IsBusy = true;

                Application.Current.Properties["Datos"] = 1;

               

                var url = "/api/PA_bsc_Producto_Uso_2";
                var service =
                    new HttpHelper<ProductosUso>();
                var productos = await service.GetRestServiceDataAsync(url);

                ProductoUsos = new ObservableCollection<ProductoUso>(productos.Table);

              
                foreach (var item in ProductoUsos)
                {
                    await App.Database.SaveDbProductos(new ProductoUso
                    {
                        Producto = item.Producto,
                        Descripcion = item.Descripcion,
                        Producto_Uso = item.Producto_Uso
                    });

                }

                IsBusy = false;

            }
        }
    }
}
