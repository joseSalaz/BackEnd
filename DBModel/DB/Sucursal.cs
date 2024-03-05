using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class Sucursal
{
    public int IdSucursal { get; set; }

    public string? Ubicacion { get; set; }

    public virtual ICollection<DocEntrada> DocEntrada { get; set; } = new List<DocEntrada>();

    public virtual ICollection<DocSalida> DocSalida { get; set; } = new List<DocSalida>();

    public virtual ICollection<Kardex> Kardices { get; set; } = new List<Kardex>();
}
