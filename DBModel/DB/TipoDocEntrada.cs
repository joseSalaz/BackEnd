using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class TipoDocEntrada
{
    public int IdTipoDocEntrada { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<DocEntrada> DocEntrada { get; set; } = new List<DocEntrada>();
}
