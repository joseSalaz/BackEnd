using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class TipoDocEntradaRequest
    {
        public int IdTipoDocEntrada { get; set; }

        public string? Descripcion {get; set;}
    }
}
