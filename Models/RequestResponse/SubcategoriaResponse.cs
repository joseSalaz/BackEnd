using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class SubcategoriaResponse
    {
        public int Id { get; set; }

        public string? Descripcion { get; set; }

        public int IdCategoria { get; set; }
    }
}
