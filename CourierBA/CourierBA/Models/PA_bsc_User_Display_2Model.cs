using System;
using System.Collections.Generic;
using System.Text;

namespace CourierBA.Models
{
    public class PA_bsc_User_Display_2Model
    {

        public int User_Display { get; set; }
        public int? User_Display_Father { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public bool? Visible { get; set; }
        public int? Rol { get; set; }
        public string Display { get; set; }
        public int? Application { get; set; }
        public int? Param { get; set; }
        public int? Orden { get; set; }
        public string Display_URL { get; set; }
        public string Display_Menu { get; set; }


    }

    // PA_bsc_User_Display_2 myDeserializedClass = JsonConvert.DeserializeObject<PA_bsc_User_Display_2>(myJsonResponse); 


    public class PA_bsc_User_Display_2
    {
        public List<PA_bsc_User_Display_2Model> Table { get; set; }
    }

}
