using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class PublicoObjetivo
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public int? Cantidad { get; set; }

    public virtual ICollection<Precio> Precios { get; set; } = new List<Precio>();
}
