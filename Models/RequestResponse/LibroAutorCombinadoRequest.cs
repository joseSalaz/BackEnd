using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class LibroAutorCombinadoRequest
    {
        public LibroRequest Libro { get; set; }
        public AutorRequest Autor { get; set; }
        public LibroAutorRequest LibroAutor { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
    }

}
