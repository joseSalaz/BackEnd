using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class Precio
{
    public int IdPrecios { get; set; }

    public decimal? PrecioVenta { get; set; }

    public decimal? PorcUtilidad { get; set; }

    public int IdLibro { get; set; }

    public int? IdPublicoObjetivo { get; set; }

    public virtual Libro IdLibroNavigation { get; set; } = null!;

    public virtual PublicoObjetivo? IdPublicoObjetivoNavigation { get; set; }
}
