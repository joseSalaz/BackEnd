using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class DocEntradaResponse
    {
        public int IdDocEntrada { get; set; }

        public DateTime? Fecha { get; set; }

        public string? NroDocEntrada { get; set; }

        public int IdTipoDocEntrada { get; set; }

        public int IdSucursal { get; set; }

        public int IdProveedor { get; set; }
    }
}
