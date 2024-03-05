using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string? Categoria1 { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
