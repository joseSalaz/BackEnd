using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class LibroconautorRequest
    {
        public LibroRequest Libro { get; set; }
        public AutorRequest? Autor { get; set; }
    }
}
