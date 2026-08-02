using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class DatosGeneraleResponse
    {
        public int IdDatosGenerales { get; set; }

        public string? Ruc { get; set; }

        public string? TelefonoContacto { get; set; }

        public string? RazonSocial { get; set; }
    }
}
