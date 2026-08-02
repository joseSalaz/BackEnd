using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class FavoritoResponse
    {
        public int IdFavorito { get; set; }
        public int IdPersona { get; set; }
        public int IdLibro { get; set; }
        public DateTime FechaAgregado { get; set; }

        // Propiedades aplanadas que AutoMapper llenará desde IdLibroNavigation
        public string? TituloLibro { get; set; }
        public string? ImagenLibro { get; set; }
    }
}
