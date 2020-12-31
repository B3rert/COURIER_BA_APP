using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CourierBA.Models
{
    public class Menu : BindableBase
    {
        private string _icon;
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _pageName;
        public string PageName
        {
            get => _pageName;
            set => SetProperty(ref _pageName, value);
        }

        private int _applicationId;
        public int ApplicationId
        {
            get => _applicationId;
            set => SetProperty(ref _applicationId, value);
        }

        private ObservableCollection<Menu> _children;

        public ObservableCollection<Menu> Children
        {
            get => _children;
            set => SetProperty(ref _children, value);
        }
    }
}
