using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class Autor
{
    public int IdAutor { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public int? Codigo { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<LibroAutor> LibroAutors { get; set; } = new List<LibroAutor>();
}
