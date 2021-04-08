using CourierBA.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace CourierBA.ViewModels
{
    public class ListTrackingUserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ListTrackingUserViewModel()
        {

        }

        public Command SelectedCommand
        {
            get
            {
                return new Command(ItemSeleccionado);
            }
        }

        private void ItemSeleccionado(object obj)
        {
            var _app = App.Current.MainPage;

            _app.Navigation.PushAsync(new TrackingStatusPage("89749687496874"));

        }
    }
}
