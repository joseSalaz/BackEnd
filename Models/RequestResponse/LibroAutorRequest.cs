using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class LibroAutorRequest
    {
        public int IdLibroAutor { get; set; }

        public int? IdLibroR { get; set; }

        public int? IdAutorR { get; set; }
    }
}
