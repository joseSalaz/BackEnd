using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class PersonaResponse
    {
        public int IdPersona { get; set; }

        public string? Cargo { get; set; }

        public string? Nombres { get; set; }

        public string? Correo { get; set; }

        public string? TipoDocumento { get; set; }

        public int? NumDocumento { get; set; }

        public string? RazonSocial { get; set; }
    }
}
