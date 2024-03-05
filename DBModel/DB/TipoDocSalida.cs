using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class TipoDocSalida
{
    public int IdTipoDocSalida { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<DocSalida> DocSalida { get; set; } = new List<DocSalida>();
}
