using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class LibroAutor
{
    public int IdLibroAutor { get; set; }

    public int IdAutor { get; set; }

    public int IdLibro { get; set; }

    public virtual Autor IdAutorNavigation { get; set; } = null!;

    public virtual Libro IdLibroNavigation { get; set; } = null!;
}
