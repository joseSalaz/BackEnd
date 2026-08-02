using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class ProveedorResponse
    {
        public int IdProveedor { get; set; }

        public string? RazonSocial { get; set; }

        public string? Ruc { get; set; }

        public string? Direccion { get; set; }

        public int IdTipoProveedor { get; set; }
    }
}
