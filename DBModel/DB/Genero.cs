using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class Genero
{
    public int IdGenero { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<LibroGenero> LibroGeneros { get; set; } = new List<LibroGenero>();
}
