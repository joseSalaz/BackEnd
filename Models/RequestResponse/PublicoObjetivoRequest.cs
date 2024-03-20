using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class PublicoObjetivoRequest
    {
        public int Id { get; set; }

        public string? Descripcion { get; set; }

        public int? Cantidad { get; set; }
    }
}
