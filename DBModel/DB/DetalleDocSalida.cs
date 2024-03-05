using System;
using System.Collections.Generic;

namespace DBModel.DB;

public partial class DetalleDocSalida
{
    public int IdDetalleSalida { get; set; }

    public int IdDocSalida { get; set; }

    public int IdLibro { get; set; }

    public int? Cantidad { get; set; }

    public string? Motivo { get; set; }

    public virtual DocSalida IdDocSalidaNavigation { get; set; } = null!;

    public virtual Libro IdLibroNavigation { get; set; } = null!;
}
