using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DBModel.DB;

public partial class Kardex
{
    public int IdSucursal { get; set; }

    public int IdLibro { get; set; }

    public int? CantidadSalida { get; set; }

    public int? CantidadEntrada { get; set; }

    public int? Stock { get; set; }

    public decimal? UltPrecioCosto { get; set; }
    [JsonIgnore]
    public virtual Libro IdLibroNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;
}
