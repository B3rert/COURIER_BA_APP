using System;
using System.Collections.Generic;
using System.Text;

namespace CourierBA.Models
{
    public class MenuResponse
    {
        public int User_Display { get; set; }
        public int? User_Display_Father { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public bool Visible { get; set; }
        public int Rol { get; set; }
        public string Display { get; set; }
        public int Application { get; set; }
        public int? Param { get; set; }
        public int Orden { get; set; }
        public string Display_URL { get; set; }
        public string Display_Menu { get; set; }
        public string Display_URL_Alter { get; set; }
    }
}
