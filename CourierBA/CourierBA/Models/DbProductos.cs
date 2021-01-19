using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourierBA.Models
{
   public  class DbProductos
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Producto { get; set; }
        public string Descripcion { get; set; }
        public int? Producto_Uso { get; set; }

    }
}
