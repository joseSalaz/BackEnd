using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
   public class LibroRequest
    {
        public int IdLibro { get; set; }

        public string? Titulo { get; set; }

        public string? Isbn { get; set; }

        public string? Tamanno { get; set; }

        public string? Descripcion { get; set; }

        public string? Condicion { get; set; }

        public string? Impresion { get; set; }

        public string? TipoTapa { get; set; }

        public bool? Estado { get; set; }

        public int IdSubcategoria { get; set; }

        public int IdTipoPapel { get; set; }

        public int IdProveedor { get; set; }

    }
}
