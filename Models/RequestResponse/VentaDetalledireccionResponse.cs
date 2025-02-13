using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class VentaDetalledireccionResponse
    {
        public int Id_Ventas { get; set; }
        public decimal Total_Precio { get; set; }
        public string Tipo_Comprobante { get; set; }
        public DateTime Fecha_Venta { get; set; }
        public string NroComprobante { get; set; }

        public int Id_Persona { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Numero_Documento { get; set; }
        public string Tipo_Documento { get; set; }
        

        public int? Id_Direccion { get; set; }
        public string Direccion { get; set; }
        public string Referencia { get; set; }
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public string CodigoPostal { get; set; }
    }
}
