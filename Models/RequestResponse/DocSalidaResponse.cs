using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class DocSalidaResponse
    {
        public int IdDocSalida { get; set; }

        public int? NroDocSalida { get; set; }

        public DateTime? Fecha { get; set; }

        public int IdSucursal { get; set; }

        public int IdTipoDocSalida { get; set; }
    }
}
