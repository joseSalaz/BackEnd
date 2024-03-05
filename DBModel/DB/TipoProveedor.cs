using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class TipoProveedor
{
    public int IdTipoProveedor { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Proveedor> Proveedors { get; set; } = new List<Proveedor>();
}
