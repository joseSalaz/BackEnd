using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel.DB
{
    public partial class Favorito
    {
        public int IdFavorito { get; set; }
        public int IdPersona { get; set; }
        public int IdLibro { get; set; }
        public DateTime FechaAgregado { get; set; }

        // Propiedades de navegación virtuales para EF Core
        public virtual Libro IdLibroNavigation { get; set; } = null!;
        public virtual Persona IdPersonaNavigation { get; set; } = null!;
    }
}
