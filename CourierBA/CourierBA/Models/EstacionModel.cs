using System;
using System.Collections.Generic;
using System.Text;

namespace CourierBA.Models
{
    public class EstacionModel
    {
        public int? Estacion_Trabajo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class Estacion
    {
        public List<EstacionModel> Table { get; set; }
    }

}
