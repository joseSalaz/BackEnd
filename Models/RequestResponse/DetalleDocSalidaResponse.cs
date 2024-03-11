using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class DetalleDocSalidaResponse
    {
        public int IdDetalleSalida { get; set; }

        public int IdDocSalida { get; set; }

        public int IdLibro { get; set; }

        public int? Cantidad { get; set; }

        public string? Motivo { get; set; }
    }
}
