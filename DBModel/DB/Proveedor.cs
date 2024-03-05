using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class Proveedor
{
    public int IdProveedor { get; set; }

    public string? RazonSocial { get; set; }

    public string? Ruc { get; set; }

    public string? Direccion { get; set; }

    public int IdTipoProveedor { get; set; }

    public virtual ICollection<DocEntrada> DocEntrada { get; set; } = new List<DocEntrada>();

    public virtual TipoProveedor IdTipoProveedorNavigation { get; set; } = null!;

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
