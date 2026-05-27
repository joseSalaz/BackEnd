using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.Response
{
    public class LibroDataResponse
    {
        public int IdLibro { get; set; }

        public string titulo { get; set; }

        public string ISBN { get; set; }

        public string Tamanno { get; set; }

        public string Descripcion { get; set; }

        public string Condicion { get; set; }

        public string Impresion { get; set; }

        public string Tipo_Tapa { get; set; }

        public string estado { get; set; }

        public string Categoria { get; set; }

        public string Subcategoria { get; set; }

        public string TipoPapel { get; set; }

        public string Razon_Social { get; set; }

        public string Imagen { get; set; }

        public string autores { get; set; }
    }
}
