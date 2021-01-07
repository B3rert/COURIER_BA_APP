using System;
using System.Collections.Generic;
using System.Text;

namespace CourierBA.Models
{
    public class ProductosUso
    {
        public ProductoUso[] Table { get; set; }
    }

    public class ProductoUso
    {
        public int Producto { get; set; }
        public string Descripcion { get; set; }
        public int? Producto_Uso { get; set; }
    }
}
