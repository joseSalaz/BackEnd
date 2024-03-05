using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class LibroGenero
{
    public int IdGenero { get; set; }

    public int IdLibro { get; set; }

    public virtual Genero IdGeneroNavigation { get; set; } = null!;

    public virtual Libro IdLibroNavigation { get; set; } = null!;
}
