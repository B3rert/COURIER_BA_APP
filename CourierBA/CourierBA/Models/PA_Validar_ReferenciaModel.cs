using System;
using System.Collections.Generic;
using System.Text;

namespace CourierBA.Models
{
    // PA_Validar_Referencia myDeserializedClass = JsonConvert.DeserializeObject<PA_Validar_Referencia>(myJsonResponse); 

    public class PA_Validar_ReferenciaModel
    {
        public string Mensaje { get; set; }
    }

    public class PA_Validar_Referencia
    {
        public List<PA_Validar_ReferenciaModel> Table { get; set; }
    }


}
