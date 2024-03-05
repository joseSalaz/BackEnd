using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class LibroGeneroResponse
    {
        public int IdLibroDeposito { get; set; }

        public string? Titulo { get; set; }

        public int? IdGeneroR { get; set; }

        public int? IdLibroR { get; set; }
    }
}
