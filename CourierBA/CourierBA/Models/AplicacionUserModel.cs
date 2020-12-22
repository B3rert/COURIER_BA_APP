using System;
using System.Collections.Generic;
using System.Text;

namespace CourierBA.Models
{
    public class AplicionUserModel
    {
        public int? Application { get; set; }
        public string Observacion_1 { get; set; }
    }

    public class Aplicacion
    {
        public List<AplicionUserModel> Table { get; set; }
    }
}
