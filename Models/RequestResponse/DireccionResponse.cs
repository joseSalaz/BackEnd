using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class DireccionResponse
    {
        public int IdDireccion { get; set; }

        public int IdPersona { get; set; }

        public string Direccion1 { get; set; } = null!;

        public string? Referencia { get; set; }

        public string Departamento { get; set; } = null!;

        public string Provincia { get; set; } = null!;

        public string Distrito { get; set; } = null!;

        public string? CodigoPostal { get; set; }

        public bool EsPredeterminada { get; set; }

        public DateTime? FechaCreacion { get; set; }

    }
}
