using Acr.UserDialogs;
using CourierBA.Services;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CourierBA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void btnBuscarCodigo_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnEscanearCodigo_Clicked(object sender, EventArgs e)
        {
            try
            {
                var scanner = DependencyService.Get<IQrScanningService>();
                var result = await scanner.ScanAsync();
                if (result != null)
                {
                    txtCodigo.Text = result;
                }
                else
                {
                    await DisplayAlert("404", "No se han encontrados datos", "Aceptar");
                    return;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "No se ha podigo escanear el codigo de barras", "Aceptar");
                return;
            }

        }

     
        //File picker
        private async void btnSelectFile_Clicked(object sender, EventArgs e)
        {
            var pickResult = await FilePicker.PickMultipleAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Pick an image(s)"
            });

            if (pickResult != null)
            {
                collectionImages.ItemsSource = null;
                var imageList = new List<ImageSource>();
                foreach (var image in pickResult)
                {
                    var stream = await image.OpenReadAsync();
                    imageList.Add(ImageSource.FromStream(() => stream));

                }
                collectionImages.ItemsSource = imageList;
                lblNameFileSelect.Text = "Archivos selecionados: " + pickResult.Count().ToString();
            }
        }

        private void btnClearImage_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading(title: "Cargando...");
            lblNameFileSelect.Text = "No se elegió ningun archivo";
            collectionImages.ItemsSource = null;
            UserDialogs.Instance.HideLoading();
        }

        private async void btnTomarFoto_Clicked(object sender, EventArgs e)
        {
            var cameraOptions = new StoreCameraMediaOptions();
            cameraOptions.PhotoSize = PhotoSize.Medium;
            cameraOptions.SaveToAlbum = true;
            var photo =
                await Plugin.Media.CrossMedia.Current
                      .TakePhotoAsync(cameraOptions);



            if (photo != null)
            {
                collectionImages.ItemsSource = null;
                var imageList = new List<ImageSource>();
                imageList.Add(ImageSource.FromStream(() => { return photo.GetStream(); }));


                collectionImages.ItemsSource = imageList;
                lblNameFileSelect.Text = "Archivos selecionados: 1";

            //  cameraImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            //lblNameFileSelect.Text = "Archivos selecionados: 1";

            }
        }
    }
}