using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class DetalleDocEntrada
{
    public int IdDetalleDocEntrada { get; set; }

    public decimal? PrecioCosto { get; set; }

    public int? Cantidad { get; set; }

    public int IdDocEntrada { get; set; }

    public decimal? PorcentajeUtil { get; set; }

    public int? MinStock { get; set; }

    public int IdLibro { get; set; }

    public virtual DocEntrada IdDocEntradaNavigation { get; set; } = null!;

    public virtual Libro IdLibroNavigation { get; set; } = null!;
}
