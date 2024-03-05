using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class TipoPapel
{
    public int IdTipoPapel { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
