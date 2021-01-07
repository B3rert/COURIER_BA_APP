using System;
using System.Collections.Generic;
using System.Text;

namespace CourierBA.Models
{

    // PA_bsc_Moneda_2 myDeserializedClass = JsonConvert.DeserializeObject<PA_bsc_Moneda_2>(myJsonResponse); 

    public class PA_bsc_Moneda_2Model
    {
        public int Moneda { get; set; }
        public string Nombre { get; set; }
        public string Simbolo { get; set; }
        public string Flag_File { get; set; }
        public string Descripcion { get; set; }
        public string Campo_1 { get; set; }
        public string Campo_2 { get; set; }
        public string Campo_3 { get; set; }
    }

    public class PA_bsc_Moneda_2
    {
        public List<PA_bsc_Moneda_2Model> Table { get; set; }
    }
}
