using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
   public class AutorRequest
    {
        public int IdAutor { get; set; }

        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public int? Codigo { get; set; }

        public string? Descripcion { get; set; }
    }
}
