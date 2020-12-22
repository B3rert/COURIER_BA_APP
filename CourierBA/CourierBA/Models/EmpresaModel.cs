using System;
using System.Collections.Generic;
using System.Text;

namespace CourierBA.Models
{
    public class EmpresaModel
    {
        public int? Empresa { get; set; }
        public string Empresa_Nombre { get; set; }
        public string Razon_Social { get; set; }
        public string Empresa_NIT { get; set; }
        public string Empresa_Direccion { get; set; }
        public string Numero_Patronal { get; set; }
        public int? Estado { get; set; }
        public string Campo_1 { get; set; }
        public string Campo_2 { get; set; }
        public string Campo_3 { get; set; }
        public string Campo_4 { get; set; }
        public string Campo_5 { get; set; }
        public string Campo_6 { get; set; }
        public string Campo_7 { get; set; }
        public string Campo_8 { get; set; }
    }
    public class Empresa
    {
        public List<EmpresaModel> Table { get; set; }
    }
}
