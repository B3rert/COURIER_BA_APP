using System;
using System.Collections.Generic;
using System.Text;

namespace CourierBA.Models
{

    public class Trackings
    {
        public Tracking[] Table { get; set; }
    }

    public class Tracking
    {
        public int Referencia { get; set; }
        public int Empresa { get; set; }
        public string Descripcion { get; set; }
        public string Referencia_Id { get; set; }
        public int Raiz { get; set; }
        public int Nivel { get; set; }
        public int Referencia_Padre { get; set; }
        public string Observacion { get; set; }
        public int Pais { get; set; }
        public DateTime Fecha_Hora { get; set; }
        public string UserName { get; set; }
        public object Fecha_Ini { get; set; }
        public object Fecha_Fin { get; set; }
        public int Tipo_Referencia { get; set; }
        public object M_Fecha_Hora { get; set; }
        public object M_UserName { get; set; }
        public int Estado { get; set; }
        public object Fecha_Evento { get; set; }
        public bool Importacion { get; set; }
        public object Cuenta_Correntista { get; set; }
        public float Monto_1 { get; set; }
        public object Monto_2 { get; set; }
        public bool Caja_Chica { get; set; }
        public object Cargo { get; set; }
        public object Abono { get; set; }
        public string Nom_Pais { get; set; }
        public object Nom_Cuenta_Correntista { get; set; }
        public object HAWB_HBL { get; set; }
        public object Suplidor { get; set; }
        public object Consignatario { get; set; }
        public float Peso { get; set; }
        public object Volumen { get; set; }
        public object Contenedor { get; set; }
        public string Pieza { get; set; }
        public object Tipo_Pieza { get; set; }
        public object Contenido { get; set; }
        public object MAWB { get; set; }
        public object Pais_Destino { get; set; }
        public object Cuenta_Correntista_Origen { get; set; }
        public object Id_Documento_Origen { get; set; }
        public object Recepcion_Nombre { get; set; }
        public object Recepcion_Id { get; set; }
        public object Tipo_Pago { get; set; }
        public float Cantidad { get; set; }
        public int Producto { get; set; }
        public object Unidad_Medida { get; set; }
        public object Observacion_2 { get; set; }
        public object Observacion_3 { get; set; }
        public object TEU { get; set; }
        public object Elemento_Asignado { get; set; }
        public object Banco { get; set; }
        public object Monto_Cuota { get; set; }
        public object Monto_Financiar { get; set; }
        public object Monto_Enganche { get; set; }
        public object Id_Cuenta { get; set; }
        public object Actividad { get; set; }
        public object Categoria { get; set; }
        public object SubCategoria { get; set; }
        public object Ref_Serie { get; set; }
        public object Hotel_Estado { get; set; }
        public object Permitir_CxC { get; set; }
        public int Orden { get; set; }
        public object Moneda { get; set; }
        public object Cuenta_Cta { get; set; }
        public object Bodega_Ubicacion { get; set; }
        public object Opc_Detalle { get; set; }
        public int Referencia_Id_1 { get; set; }
    }

}
