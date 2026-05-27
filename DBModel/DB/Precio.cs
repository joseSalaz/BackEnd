using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DBModel.DB;

public partial class Precio
{
    public int IdPrecios { get; set; }

    public decimal? PrecioVenta { get; set; }

    public decimal? PorcUtilidad { get; set; }

    public int IdLibro { get; set; }

    public int? IdPublicoObjetivo { get; set; }
    [JsonIgnore]
    public virtual Libro IdLibroNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual PublicoObjetivo? IdPublicoObjetivoNavigation { get; set; }
}
